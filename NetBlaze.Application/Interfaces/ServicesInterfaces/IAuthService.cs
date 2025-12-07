using NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.LogIn.ResponseDTOs;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Request;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<LogInResponseDTO>> LogInAsync(LogInRequestDTO logInRequestDto, CancellationToken cancellationToken = default);

        Task<ApiResponse<RegisterResponseDTO>> RegisterAsync(RegisterRequestDTO registerRequestDto, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> UpdateUserAsync(UpdateUserRequestDTO updateUserRequestDto, CancellationToken cancellationToken = default);
    }
}
