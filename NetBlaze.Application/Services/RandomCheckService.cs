using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.Dtos.RandomlyCheck;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;
using System.Security.Cryptography;

namespace NetBlaze.Application.Services
{

    public class RandomCheckService : IRandomCheckService
    {
        private readonly UserManager<User> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configration;

        public RandomCheckService(UserManager<User> userManager, IUnitOfWork unitOfWork, IConfiguration configuration)   
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _configration = configuration;
        }

        public async Task<ApiResponse<RandomlyCheckResponseDTO>> GenerateOtpRecord(long userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync<User>(false, userId, cancellationToken);
            if (user == null)
                return ApiResponse<RandomlyCheckResponseDTO>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.OK);

            var OTP = GenerateSecureOtp(6);

            var EXP = _configration.GetSection(OTP);

            //var EXPInMin =EXP["ExpirationDeurationInMinutes"];

            var OtpRecord = new RandomlyCheck
            {
                CreatedAt = DateTime.UtcNow,
                ExpiredAt = DateTime.UtcNow.AddMinutes(30),
                UserId = userId,
                Status = 0,
                Otp = OTP

            };
           await _unitOfWork.Repository.AddAsync<RandomlyCheck, int>(OtpRecord, cancellationToken);

           var rows =await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            if(rows > 0)
            {
                var dto = new RandomlyCheckResponseDTO
                {
                    Status = 0,
                    CreatedAt = OtpRecord.CreatedAt,
                    ExpiredAt = OtpRecord.ExpiredAt,
                    UserId = userId,
                    Otp = OTP
                };
            return ApiResponse<RandomlyCheckResponseDTO>.ReturnSuccessResponse(dto);
            }

            return ApiResponse<RandomlyCheckResponseDTO>.ReturnFailureResponse(Messages.ErrorOccurredInServer, HttpStatusCode.InternalServerError);


        }

        public async Task<ApiResponse<bool>> OtpValidation(long userId,string otp, CancellationToken cancellationToken)
        {
            var otpRow = await _unitOfWork.Repository.GetSingleAsync<RandomlyCheck>(true, x=> x.UserId == userId, cancellationToken);

            if (otpRow == null) 
                ApiResponse<bool>.ReturnFailureResponse();

            var result = CheckOtpRecord(otpRow, userId, otp);

            if (result)
                return ApiResponse<bool>.ReturnSuccessResponse(true);

            return ApiResponse<bool>.ReturnFailureResponse();
   
        }

        #region HELPER
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

        
        private static bool CheckOtpRecord(RandomlyCheck randomlyCheck, long userId, string otp)
        {
            if (randomlyCheck.Otp != otp) return false;

            if (randomlyCheck.UserId != userId) return false;

            if(randomlyCheck.Status == 1) return false;

            if(randomlyCheck.ExpiredAt >= DateTime.Now) return false;

            return true;
        }
        #endregion
    }
}
