using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Application.Mappings;
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
            user.IsActive = false;

            _unitOfWork.Repository.Update(user);
            await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            return ApiResponse<bool>.ReturnSuccessResponse(
                true,
                Messages.UserAlreadyDeleted, HttpStatusCode.NotFound
            );
        }


       
        public async Task<ApiResponse<UserResponseDTO>> UpdateUserAsync(UpdateUserDTO updateUserDTO, CancellationToken cancellationToken)
        {
            var user = _unitOfWork.Repository.GetById<User>(false, updateUserDTO.UserId);

            
            if (user == null)
                return ApiResponse<UserResponseDTO>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.NotFound);

            user.UserName = updateUserDTO.UserName;
            user.DisplayName = updateUserDTO.Displayname;
            user.Email = updateUserDTO.UserEmail;

            await _unitOfWork.Repository.CompleteAsync();

            var NewUser = new UserResponseDTO
            {
                UserName = updateUserDTO.UserName,
                UserEmail = updateUserDTO.UserEmail,
                DisplayName = updateUserDTO.Displayname

            };

            return ApiResponse<UserResponseDTO>.ReturnSuccessResponse(NewUser, Messages.UserUpdated, HttpStatusCode.OK);

            
             
        }

          public async Task<ApiResponse<object>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var usersList = _unitOfWork.Repository.GetQueryable<User>();

            usersList.PaginatedListAsync(pageNumber, pageSize);

            var users = usersList.Select(u => new UserResponseDTO
            {
                DisplayName = u.DisplayName,
                UserName = u.UserName,
                UserEmail = u.Email
            });

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.InvalidEmail, HttpStatusCode.OK);
        }
    }
}
