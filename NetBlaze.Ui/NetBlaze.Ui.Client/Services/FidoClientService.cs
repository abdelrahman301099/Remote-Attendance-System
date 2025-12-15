using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.Ui.Client.Services.CommonServices;

namespace NetBlaze.Ui.Client.Services
{
    public class FidoClientService
    {
        private readonly ExternalHttpClientWrapper _http;

        public FidoClientService(ExternalHttpClientWrapper http)
        {
            _http = http;
        }

        public async Task<ApiResponse<object>> BeginRegistrationAsync(string email)
        {
            return await _http.PostAsJsonAsync<object, ApiResponse<object>>($"api/Fido2/begin-registration?email={Uri.EscapeDataString(email)}", new { });
        }

        public async Task<ApiResponse<bool>> CompleteRegistrationAsync(string email, string attestationJson)
        {
            var payload = new { Email = email, AttestationJson = attestationJson };
            return await _http.PostAsJsonAsync<object, ApiResponse<bool>>("api/Fido2/complete-registration", payload);
        }

        public async Task<ApiResponse<object>> BeginAssertionAsync(string email)
        {
            return await _http.PostAsJsonAsync<object, ApiResponse<object>>($"api/Fido2/begin-assertion?email={Uri.EscapeDataString(email)}", new { });
        }

        public async Task<ApiResponse<bool>> CompleteAssertionAsync(string email, string assertionJson)
        {
            var payload = new { Email = email, AssertionJson = assertionJson };
            return await _http.PostAsJsonAsync<object, ApiResponse<bool>>("api/Fido2/complete-assertion", payload);
        }
    }
}
