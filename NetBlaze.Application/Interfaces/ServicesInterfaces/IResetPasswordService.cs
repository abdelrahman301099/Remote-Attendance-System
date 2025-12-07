using NetBlaze.SharedKernel.Dtos.LogIn.ResponseDTOs;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Request;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IResetPasswordService
    {

        Task<ApiResponse<bool>> SendPasswordResetEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<ApiResponse<bool>> VerifyResetCodeAsync(string email, string code, CancellationToken cancellationToken = default);
        Task<ApiResponse<ResetPasswordResponseDTO>> ResetPasswordAsync(ResetPasswordRequestDto requestDto, CancellationToken cancellationToken = default);

    }
}
