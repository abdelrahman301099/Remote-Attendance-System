using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.User.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.User.ResponseDTOs;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class UserController : BaseNetBlazeController, IUserService
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [Authorize(Policy = "CanManageUsers")]
        [HttpDelete("{userId}")]
        public async Task<ApiResponse<bool>> DeleteUserAsync(long UserId, CancellationToken cancellationToken)
        {
            return await _userService.DeleteUserAsync(UserId, cancellationToken);
        }

        [HttpGet]
       public async Task<ApiResponse<object>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            return await _userService.GetAllUsersAsync(pageNumber, pageSize);
        }

        [Authorize(Policy = "HRorAdmin")]
        [HttpPut]
        public async Task<ApiResponse<UserResponseDTO>> UpdateUserAsync([FromBody]UpdateUserDTO updateUserDTO, CancellationToken cancellationToken)
        {
            return await _userService.UpdateUserAsync(updateUserDTO, cancellationToken);
        }
    }
}
