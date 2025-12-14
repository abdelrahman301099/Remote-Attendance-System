using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.LogIn.ResponseDTOs;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Request;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;

namespace NetBlaze.Api.Controllers
{
    public class AuthController : BaseNetBlazeController, IAuthService
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
           
            return await _authService.RegisterAsync(dto, cancellationToken);

        }

        [HttpPost("login")]
        public async Task<ApiResponse<LogInResponseDTO>> LogInAsync([FromBody] LogInRequestDTO dto, CancellationToken cancellationToken)
        {
            return await _authService.LogInAsync(dto, cancellationToken);
          
        }

        [HttpPost("send-code")]
        public async Task<ApiResponse<bool>> SendPasswordResetEmailAsync([FromBody] SendRequestCodeDTO request, CancellationToken cancellationToken)
        {
            return await _passwordResetService.SendPasswordResetEmailAsync(request.Email, cancellationToken);
         }

        [HttpPost("verify-code")]
        public async Task<ApiResponse<bool>> VerifyCode([FromBody] VerifyCodeRequestDTO request, CancellationToken cancellationToken)
        {
            return await _passwordResetService.VerifyResetCodeAsync(request.Email, request.Code, cancellationToken);
           
        }

        [HttpPost("reset-password")]
        public async Task<ApiResponse<ResetPasswordResponseDTO>> ResetPasswordAsync([FromBody] ResetPasswordRequestDto request, CancellationToken cancellationToken)
        {
            return await _passwordResetService.ResetPasswordAsync(request, cancellationToken);


        }

      
    }
}
