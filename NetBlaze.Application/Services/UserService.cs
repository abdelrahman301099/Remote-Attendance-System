﻿using Microsoft.AspNetCore.Identity;
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
        private readonly RoleManager<Role> _roleManager;
        

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
            _user = userManager;
            _roleManager = roleManager;
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
            if (!long.TryParse(updateUserDTO.UserId, out var uid))
            {
                return ApiResponse<UserResponseDTO>.ReturnFailureResponse(Messages.InvalidRequest, HttpStatusCode.BadRequest);
            }

            var user = await _unitOfWork.Repository.GetByIdAsync<User>(false, uid, cancellationToken).ConfigureAwait(false);

            
            if (user == null)
                return ApiResponse<UserResponseDTO>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.NotFound);

            user.UserName = updateUserDTO.UserName!;
            user.DisplayName = updateUserDTO.Displayname!;
            user.Email = updateUserDTO.UserEmail!;

            var updateResult = await _user.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                var errors = updateResult.Errors.Select(e => e.Description).ToArray();
                return ApiResponse<UserResponseDTO>.ReturnFailureResponse(Messages.ErrorOccurredInServer, HttpStatusCode.BadRequest, errors);
            }

            if (updateUserDTO.RoleId.HasValue)
            {
                var role = await _roleManager.FindByIdAsync(updateUserDTO.RoleId.Value.ToString());
                if (role == null)
                {
                    return ApiResponse<UserResponseDTO>.ReturnFailureResponse("Invalid Role Id", HttpStatusCode.BadRequest);
                }

                var currentRoles = await _user.GetRolesAsync(user);
                if (currentRoles.Any())
                {
                    var removeResult = await _user.RemoveFromRolesAsync(user, currentRoles);
                    if (!removeResult.Succeeded)
                    {
                        var errs = removeResult.Errors.Select(e => e.Description).ToArray();
                        return ApiResponse<UserResponseDTO>.ReturnFailureResponse(Messages.ErrorAssigningRole, HttpStatusCode.BadRequest, errs);
                    }
                }

                var addResult = await _user.AddToRoleAsync(user, role.Name);
                if (!addResult.Succeeded)
                {
                    var errs = addResult.Errors.Select(e => e.Description).ToArray();
                    return ApiResponse<UserResponseDTO>.ReturnFailureResponse(Messages.ErrorAssigningRole, HttpStatusCode.BadRequest, errs);
                }
            }

            var NewUser = new UserResponseDTO
            {
                UserName = updateUserDTO.UserName,
                UserEmail = updateUserDTO.UserEmail,
                DisplayName = updateUserDTO.Displayname,
                RoleId = updateUserDTO.RoleId ?? 0

            };

            return ApiResponse<UserResponseDTO>.ReturnSuccessResponse(NewUser, Messages.UserUpdated, HttpStatusCode.OK);

            
             
        }

          public async Task<ApiResponse<object>> GetAllUsersAsync(int pageNumber, int pageSize)
        {
            var usersList = _unitOfWork.Repository.GetQueryable<User>();

           

            var users = usersList.Select(u => new UserResponseDTO
            {
                DisplayName = u.DisplayName,
                UserName = u.UserName,
                UserEmail = u.Email
            });

           var items = users.PaginatedListAsync(pageNumber, pageSize);

            return ApiResponse<object>.ReturnSuccessResponse(items, "All Users" ,HttpStatusCode.OK);
        }
    }
}
