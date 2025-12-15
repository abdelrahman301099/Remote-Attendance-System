using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Fido2;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net;
using System.Security.Cryptography;

namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class Fido2Controller : BaseNetBlazeController
    {
        private readonly IFido2Service _fido2Service;

        public Fido2Controller(IFido2Service fido2Service)
        {
            _fido2Service = fido2Service;
        }

        [HttpPost("begin-registration")] 
        public async Task<ApiResponse<object>> BeginRegistration([FromQuery] string email, CancellationToken cancellationToken)
        {
            var res = await _fido2Service.BeginRegistrationAsync(email, cancellationToken);
            if (!res.Success)
            {
                return ApiResponse<object>.ReturnFailureResponse(res.Message, res.StatusCode);
            }

            var challenge = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var rpId = Request.Host.Host;
            var options = new
            {
                rp = new { name = "NetBlaze", id = rpId },
                user = new { name = email, displayName = email, id = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(email)) },
                challenge,
                pubKeyCredParams = new[] { new { type = "public-key", alg = -7 }, new { type = "public-key", alg = -257 } },
                authenticatorSelection = new { residentKey = "preferred", userVerification = "preferred" }
            };
            return ApiResponse<object>.ReturnSuccessResponse(options, "Options");
        }

        [HttpPost("complete-registration")] 
        public async Task<IActionResult> CompleteRegistration([FromBody] CompleteRegistrationRequestDTO request, CancellationToken cancellationToken)
        {
            var res = await _fido2Service.CompleteRegistrationAsync(request.Email, request.AttestationJson, cancellationToken);
            return StatusCode((int)res.StatusCode, res);
        }

        [HttpPost("begin-assertion")] 
        public async Task<ApiResponse<object>> BeginAssertion([FromQuery] string email, CancellationToken cancellationToken)
        {
            var res = await _fido2Service.BeginAssertionAsync(email, cancellationToken);
            if (!res.Success)
            {
                return ApiResponse<object>.ReturnFailureResponse(res.Message, res.StatusCode);
            }
            var challenge = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            var rpId = Request.Host.Host;
            var options = new
            {
                challenge,
                rpId,
                userVerification = "preferred"
            };
            return ApiResponse<object>.ReturnSuccessResponse(options, "Options");
        }

        [HttpPost("complete-assertion")] 
        public async Task<IActionResult> CompleteAssertion([FromBody] CompleteAssertionRequestDTO request, CancellationToken cancellationToken)
        {
            var res = await _fido2Service.ValidateAssertionForUserAsync(request.Email, request.AssertionJson, cancellationToken);
            return StatusCode((int)res.StatusCode, res);
        }
    }
}
