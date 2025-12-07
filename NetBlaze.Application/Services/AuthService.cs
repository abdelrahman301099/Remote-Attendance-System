using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using NetBlaze.SharedKernel.Dtos.LogIn.RequestDTOs;
using NetBlaze.SharedKernel.Dtos.LogIn.ResponseDTOs;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.Dtos.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Request;
using NetBlaze.SharedKernel.Dtos.ResetPassword.Response;
using Microsoft.EntityFrameworkCore;

namespace NetBlaze.Application.Services
{
    //TODO:Cmplete Localization
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtBearerService _jwtBearerService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AuthService(IUnitOfWork unitOfWork, IJwtBearerService jwtBearerService, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _unitOfWork = unitOfWork;
            _jwtBearerService = jwtBearerService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApiResponse<LogInResponseDTO>> LogInAsync(LogInRequestDTO logInRequestDto, CancellationToken cancellationToken = default)
        {

            var user = await _userManager.FindByEmailAsync(logInRequestDto.Email);

            if (user == null)
            {
                return ApiResponse<LogInResponseDTO>.ReturnFailureResponse(Messages.AttendanceNotFound, HttpStatusCode.Unauthorized);
            }

            var validPassword = await _userManager.CheckPasswordAsync(user, logInRequestDto.Password);
            if (!validPassword)
            {
                return ApiResponse<LogInResponseDTO>.ReturnFailureResponse(Messages.AttendanceNotFound, HttpStatusCode.Unauthorized);
            }
            
            var roles = await _userManager.GetRolesAsync(user);

            var tokenRequest = new GenerateTokenRequestDto(user.Id, user.UserName, user.Email, roles.ToList());
            var accessToken = _jwtBearerService.GenerateToken(tokenRequest);

            var responseDto = new LogInResponseDTO
            {
                AccessToken = accessToken,
                Expiration = DateTime.UtcNow.AddMinutes(30)
            };

            return ApiResponse<LogInResponseDTO>.ReturnSuccessResponse(responseDto);
        }




        public async Task<ApiResponse<RegisterResponseDTO>> RegisterAsync(RegisterRequestDTO registerRequestDto, CancellationToken cancellationToken = default)
        {
            var alreadyExists = await _userManager.FindByEmailAsync(registerRequestDto.Email);
            if (alreadyExists != null)
            {
                return ApiResponse<RegisterResponseDTO>.ReturnFailureResponse(
                    "User already exists", HttpStatusCode.Conflict);
            }

            var role = await _roleManager.FindByIdAsync(registerRequestDto.RoleId.ToString());
            if (role == null)
            {
                return ApiResponse<RegisterResponseDTO>.ReturnFailureResponse(
                    "Invalid Role Id", HttpStatusCode.BadRequest);
            }

            
            var user = new User
            {
                Email = registerRequestDto.Email.Trim(),
                UserName = registerRequestDto.UserName.Trim(),
                DisplayName = registerRequestDto.FullName.Trim(),
                CreatedAt = DateTimeOffset.UtcNow,  
                IsActive = true,               //check errors related to user schema      
                IsDeleted = false,                   
                EmailConfirmed = false,            
                PhoneNumberConfirmed = false,        
                TwoFactorEnabled = false,            
                LockoutEnabled = true                
            };

            
                var createResult = await _userManager.CreateAsync(user, registerRequestDto.Password);
                if (!createResult.Succeeded)
                {
                    var errors = createResult.Errors.Select(e => e.Description).ToArray();
                    return ApiResponse<RegisterResponseDTO>.ReturnFailureResponse(
                        Messages.ErrorOccurredInServer, HttpStatusCode.BadRequest, errors);
                }

                var roleResult = await _userManager.AddToRoleAsync(user, role.Name);
                if (!roleResult.Succeeded)
                {
                    
                    await _userManager.DeleteAsync(user); 

                    var errors = roleResult.Errors.Select(e => e.Description).ToArray();
                    return ApiResponse<RegisterResponseDTO>.ReturnFailureResponse(
                        "Error assigning role", HttpStatusCode.BadRequest, errors);
                }

                var responseDto = new RegisterResponseDTO
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                };

                return ApiResponse<RegisterResponseDTO>.ReturnSuccessResponse(responseDto);
            
           
        }





        public Task<ApiResponse<object>> UpdateUserAsync(UpdateUserRequestDTO updateUserRequestDto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
