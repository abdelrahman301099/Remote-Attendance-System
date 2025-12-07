using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities.Caching;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Request;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;

namespace NetBlaze.Application.Services
{
    public class ResetPasswordService : IResetPasswordService
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IDistributedCache _cache;
        private const int MAX_ATTEMPTS = 3;
        private const int CODE_EXPIRATION_MINUTES = 15;

        public ResetPasswordService(
            UserManager<User> userManager,
            IEmailSender emailSender,
            IDistributedCache cache,
            ILogger<ResetPasswordService> logger)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _cache = cache;
            
        }

        public async Task<ApiResponse<bool>> SendPasswordResetEmailAsync(
            string email,
            CancellationToken cancellationToken = default)
        {
           
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    
                    
                    return ApiResponse<bool>.ReturnSuccessResponse(
                        true,
                        "If the email exists, a reset code has been sent",
                        HttpStatusCode.OK);
                }

              
                var otp = GenerateSecureOtp(6);

                var cacheKey = $"password_reset:{user.Id}";
                var cacheData = new PasswordResetCache
                {
                    Code = otp,
                    UserId = user.Id.ToString(),
                    Email = user.Email!,
                    CreatedAt = DateTime.UtcNow,
                    AttemptCount = 0
                };

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CODE_EXPIRATION_MINUTES)
                };

                await _cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(cacheData),
                    cacheOptions,
                    cancellationToken
                );

                
                await _emailSender.SendPasswordResetCodeAsync(user.Email!, otp, cancellationToken);

               
                return ApiResponse<bool>.ReturnSuccessResponse(
                    true,
                    "Reset code sent successfully",
                    HttpStatusCode.OK);
            
           
        }

        public async Task<ApiResponse<bool>> VerifyResetCodeAsync(
            string email,
            string code,
            CancellationToken cancellationToken = default)
        {
            
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return ApiResponse<bool>.ReturnFailureResponse(
                        "Invalid request",
                        HttpStatusCode.BadRequest);
                }

                var cacheKey = $"password_reset:{user.Id}";
                var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

                if (string.IsNullOrEmpty(cachedData))
                {
                    return ApiResponse<bool>.ReturnFailureResponse(
                        "Reset code expired or not found",
                        HttpStatusCode.BadRequest);
                }

                var resetData = JsonSerializer.Deserialize<PasswordResetCache>(cachedData);
                if (resetData == null)
                {
                    return ApiResponse<bool>.ReturnFailureResponse(
                        "Invalid reset data",
                        HttpStatusCode.BadRequest);
                }

               
                if (resetData.AttemptCount >= MAX_ATTEMPTS)
                {
                    await _cache.RemoveAsync(cacheKey, cancellationToken);
                    return ApiResponse<bool>.ReturnFailureResponse(
                        "Too many attempts. Please request a new code",
                        HttpStatusCode.TooManyRequests);
                }

              
                if (resetData.Code != code)
                {
                    
                    resetData.AttemptCount++;
                    var cacheOptions = new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(CODE_EXPIRATION_MINUTES)
                    };
                    await _cache.SetStringAsync(
                        cacheKey,
                        JsonSerializer.Serialize(resetData),
                        cacheOptions,
                        cancellationToken
                    );

                    var remainingAttempts = MAX_ATTEMPTS - resetData.AttemptCount;
                    return ApiResponse<bool>.ReturnFailureResponse(
                        $"Invalid code. {remainingAttempts} attempts remaining",
                        HttpStatusCode.BadRequest);
                }

                return ApiResponse<bool>.ReturnSuccessResponse(
                    true,
                    "Code verified successfully",
                    HttpStatusCode.OK);
            
        }

        public async Task<ApiResponse<ResetPasswordResponseDTO>> ResetPasswordAsync(
            ResetPasswordRequestDto requestDto,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(requestDto.Email);
                if (user == null)
                {
                    return ApiResponse<ResetPasswordResponseDTO>.ReturnFailureResponse(
                        "Invalid request",
                        HttpStatusCode.BadRequest);
                }

                var cacheKey = $"password_reset:{user.Id}";
                var cachedData = await _cache.GetStringAsync(cacheKey, cancellationToken);

                if (string.IsNullOrEmpty(cachedData))
                {
                    return ApiResponse<ResetPasswordResponseDTO>.ReturnFailureResponse(
                        "Reset code expired or not found",
                        HttpStatusCode.BadRequest);
                }

                var resetData = JsonSerializer.Deserialize<PasswordResetCache>(cachedData);
                if (resetData == null || resetData.Code != requestDto.Code)
                {
                    return ApiResponse<ResetPasswordResponseDTO>.ReturnFailureResponse(
                        "Invalid reset code",
                        HttpStatusCode.BadRequest);
                }

                
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetResult = await _userManager.ResetPasswordAsync(user, token, requestDto.NewPassword);

                if (!resetResult.Succeeded)
                {
                    var errors = resetResult.Errors.Select(e => e.Description).ToArray();
                    return ApiResponse<ResetPasswordResponseDTO>.ReturnFailureResponse(
                        "Password reset failed",
                        HttpStatusCode.BadRequest,
                        errors);
                }

                
                await _cache.RemoveAsync(cacheKey, cancellationToken);


                return ApiResponse<ResetPasswordResponseDTO>.ReturnSuccessResponse(
                    new ResetPasswordResponseDTO
                    {
                        Succeeded = true,
                        Message = "Password reset successfully"
                    },
                    "Password reset successfully",
                    HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ApiResponse<ResetPasswordResponseDTO>.ReturnFailureResponse(
                    "An error occurred while resetting password",
                    HttpStatusCode.InternalServerError);
            }
        }

        private static string GenerateSecureOtp(int length)
        {
            const string digits = "0123456789";
            var bytes = new byte[length];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            var chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = digits[bytes[i] % digits.Length];
            }
            return new string(chars);
        }
    }
}

