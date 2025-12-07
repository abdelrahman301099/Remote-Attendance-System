using Microsoft.AspNetCore.Identity;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.Dtos.Attendance.Request;
using NetBlaze.SharedKernel.Dtos.User.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.User.ResponseDTOs;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;


namespace NetBlaze.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _user;



        public UserService(UserManager<User> userManager, IUnitOfWork unitOfWork  ) {
        
            _unitOfWork = unitOfWork;
            _user = userManager;
        
        }

        public async Task<ApiResponse<bool>> DeleteUserAsync(long userId, CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync<User>(true, userId, cancellationToken);

            if (user == null)
            {
                return ApiResponse<bool>.ReturnFailureResponse(
                    Messages.UserNotFound,
                    HttpStatusCode.NotFound
                );
            }

            if (user.IsDeleted)
            {
                return ApiResponse<bool>.ReturnFailureResponse(
                    Messages.UserNotFound, 
                    HttpStatusCode.BadRequest
                );
            }

            
            user.IsDeleted = true;
            user.DeletedAt = DateTimeOffset.UtcNow;
            user.DeletedBy = "System"; 

            _unitOfWork.Repository.Update(user);
            await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            return ApiResponse<bool>.ReturnSuccessResponse(
                true,
                Messages.UserAlreadyDeleted, HttpStatusCode.NotFound
            );
        }


        public IAsyncEnumerable<UserResponseDTO> GetAllUsersAsync()
        {
            var users = _unitOfWork.Repository.GetMultipleStream<User, UserResponseDTO>(
                 true,
                _ => true,
                x => new UserResponseDTO
                {
                    UserName = x.UserName,
                    DisplayName = x.DisplayName,
                    UserEmail = x.Email,

                    
                    RoleId = x.UserRoles
                        .Select(r => r.RoleId)
                        .FirstOrDefault()
                }
            );

            return users;
        }

    }
}
