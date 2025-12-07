using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.LogIn.ResponseDTOs;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Request;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    public class AuthController : BaseNetBlazeController //TODO:Inherit used Interfaces
    {
        private readonly IAuthService _authService;
        private readonly IResetPasswordService _passwordResetService;

        public AuthController(IAuthService authService, IResetPasswordService passwordResetService)
        {
            _authService = authService;
            _passwordResetService = passwordResetService;
        }


        [HttpPost("register")]
        public async Task<ApiResponse<RegisterResponseDTO>> RegisterAsync([FromBody] RegisterRequestDTO dto, CancellationToken cancellationToken)
        {
            var result = await _authService.RegisterAsync(dto, cancellationToken);
            var message = result.Success
                ? NetBlaze.SharedKernel.SharedResources.Messages.UserRegisteredSuccessfully
                : (result.Message == "Error assigning role"
                    ? NetBlaze.SharedKernel.SharedResources.Messages.ErrorAssigningRole
                    : NetBlaze.SharedKernel.SharedResources.Messages.ErrorOccurredInServer);

            return new ApiResponse<RegisterResponseDTO>(
                result.Data,
                result.Success,
                message,
                result.StatusCode,
                result.Error
            );
        }

        [HttpPost("login")]
        public async Task<ApiResponse<LogInResponseDTO>> LogInAsync([FromBody] LogInRequestDTO dto, CancellationToken cancellationToken)
        {
            var result = await _authService.LogInAsync(dto, cancellationToken);
            var message = result.Success
                ? result.Message
                : (result.Message == "Invalid email"
                    ? NetBlaze.SharedKernel.SharedResources.Messages.InvalidEmail
                    : (result.Message == "Invalid password"
                        ? NetBlaze.SharedKernel.SharedResources.Messages.InvalidPassword
                        : NetBlaze.SharedKernel.SharedResources.Messages.ErrorOccurredInServer));

            return new ApiResponse<LogInResponseDTO>(
                result.Data,
                result.Success,
                message,
                result.StatusCode,
                result.Error
            );
        }

        [HttpPost("send-code")]
        public async Task<ApiResponse<bool>> SendPasswordResetEmailAsync([FromBody] SendRequestCodeDTO request, CancellationToken cancellationToken)
        {
            var result = await _passwordResetService.SendPasswordResetEmailAsync(request.Email, cancellationToken);
            var message = result.Success
                ? NetBlaze.SharedKernel.SharedResources.Messages.ResetCodeSentSuccessfully
                : NetBlaze.SharedKernel.SharedResources.Messages.ErrorOccurredInServer;

            return new ApiResponse<bool>(
                result.Data,
                result.Success,
                message,
                result.StatusCode,
                result.Error
            );
        }

        [HttpPost("verify-code")]
        public async Task<ApiResponse<bool>> VerifyCode([FromBody] VerifyCodeRequestDTO request, CancellationToken cancellationToken)
        {
            var result = await _passwordResetService.VerifyResetCodeAsync(request.Email, request.Code, cancellationToken);
            var message = result.Success
                ? result.Message
                : (result.Message == "Invalid request"
                    ? NetBlaze.SharedKernel.SharedResources.Messages.InvalidRequest
                    : (result.Message == "Reset code expired or not found"
                        ? NetBlaze.SharedKernel.SharedResources.Messages.ResetCodeExpiredOrNotFound
                        : (result.Message == "Invalid reset data"
                            ? NetBlaze.SharedKernel.SharedResources.Messages.InvalidResetData
                            : NetBlaze.SharedKernel.SharedResources.Messages.ErrorOccurredInServer)));

            return new ApiResponse<bool>(
                result.Data,
                result.Success,
                message,
                result.StatusCode,
                result.Error
            );
        }

        [HttpPost("reset-password")]
        public async Task<ApiResponse<ResetPasswordResponseDTO>> ResetPasswordAsync([FromBody] ResetPasswordRequestDto request, CancellationToken cancellationToken)
        {
            var result = await _passwordResetService.ResetPasswordAsync(request, cancellationToken);
            var message = result.Success
                ? NetBlaze.SharedKernel.SharedResources.Messages.PasswordResetSuccess
                : NetBlaze.SharedKernel.SharedResources.Messages.ErrorOccurredInServer;

            return new ApiResponse<ResetPasswordResponseDTO>(
                result.Data,
                result.Success,
                message,
                result.StatusCode,
                result.Error
            );
        }


    }
}
