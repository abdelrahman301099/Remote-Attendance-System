using NetBlaze.SharedKernel.Dtos.User.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.User.ResponseDTOs;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IUserService
    {
        Task<ApiResponse<bool>> DeleteUserAsync(long UserId, CancellationToken cancellationToken);

        Task<ApiResponse<object>>  GetAllUsersAsync(int pageNumber,int pageSize );

        Task<ApiResponse<UserResponseDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO, CancellationToken cancellationToken);
    }
}
