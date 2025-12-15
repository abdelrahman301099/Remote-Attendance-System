using Microsoft.AspNetCore.Identity;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;
using Fido2NetLib;
using Fido2NetLib.Objects;

namespace NetBlaze.Application.Services
{
    public class Fido2Service : IFido2Service
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public Fido2Service(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<ApiResponse<bool>> BeginRegistrationAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<bool>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.NotFound);
            }
            return ApiResponse<bool>.ReturnSuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> CompleteRegistrationAsync(string email, string attestationJson, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<bool>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.NotFound);
            }

            string credentialId = string.Empty;
            
                using var doc = System.Text.Json.JsonDocument.Parse(attestationJson);
                var root = doc.RootElement;
                if (root.TryGetProperty("id", out var idProp))
                {
                    credentialId = idProp.GetString() ?? string.Empty;
                }
            
            

            var deviceNumber = string.Empty;

            var details = await _unitOfWork.Repository.GetSingleAsync<UserDetails>(true, x => x.Id == (user.UserDetailsId ?? 0), cancellationToken);
            if (details == null)
            {
                details = new UserDetails
                {
                    DeviceNumber = deviceNumber,
                    CertificatePassword = string.Empty,
                    Key = credentialId
                };
                await _unitOfWork.Repository.AddAsync<UserDetails, long>(details, cancellationToken).ConfigureAwait(false);
                var rows1 = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);
                if (rows1 <= 0)
                {
                    return ApiResponse<bool>.ReturnFailureResponse("Failed to persist device details");
                }
                user.UserDetailsId = details.Id;
                _unitOfWork.Repository.Update(user);
            }
            else
            {
                details.DeviceNumber = deviceNumber;
                details.Key = credentialId;
                _unitOfWork.Repository.Update(details);
            }

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);
            if (rows > 0)
            {
                return ApiResponse<bool>.ReturnSuccessResponse(true, "FIDO2 device registered");
            }
            return ApiResponse<bool>.ReturnFailureResponse("Failed to register FIDO2 device");
        }

        public async Task<ApiResponse<bool>> BeginAssertionAsync(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<bool>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.NotFound);
            }
            var details = await _unitOfWork.Repository.GetSingleAsync<UserDetails>(true, x => x.Id == (user.UserDetailsId ?? 0), cancellationToken);
            if (details == null || string.IsNullOrEmpty(details.Key))
            {
                return ApiResponse<bool>.ReturnFailureResponse("No registered FIDO2 device", HttpStatusCode.BadRequest);
            }
            return ApiResponse<bool>.ReturnSuccessResponse(true);
        }

        public async Task<ApiResponse<bool>> ValidateAssertionForUserAsync(string email, string assertionJson, CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return ApiResponse<bool>.ReturnFailureResponse(Messages.UserNotFound, HttpStatusCode.NotFound);
            }
            var details = await _unitOfWork.Repository.GetSingleAsync<UserDetails>(true, x => x.Id == (user.UserDetailsId ?? 0), cancellationToken);
            if (details == null || string.IsNullOrEmpty(details.Key))
            {
                return ApiResponse<bool>.ReturnFailureResponse("No registered FIDO2 device", HttpStatusCode.BadRequest);
            }

            string credentialId = string.Empty;
             using var doc = System.Text.Json.JsonDocument.Parse(assertionJson);
                var root = doc.RootElement;
                if (root.TryGetProperty("id", out var idProp))
                {
                    credentialId = idProp.GetString() ?? string.Empty;
                }
           

            var isSameDevice = credentialId == details.Key;
            if (!isSameDevice)
            {
                return ApiResponse<bool>.ReturnFailureResponse("FIDO2 device mismatch", HttpStatusCode.Forbidden);
            }

            return ApiResponse<bool>.ReturnSuccessResponse(true);
        }
    }
}
