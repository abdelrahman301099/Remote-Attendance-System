using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NetBlaze.SharedKernel.Dtos.Attendance.Response;
using NetBlaze.SharedKernel.Dtos.Attendance.Request;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.Ui.Client.Services;
using NetBlaze.Ui.Client.Services.CommonServices;

namespace NetBlaze.Ui.Client.Pages
{
    public partial class FidoTest : ComponentBase
    {
        [Inject] public FidoClientService Fido { get; set; } = default!;
        [Inject] public IJSRuntime JS { get; set; } = default!;
        [Inject] public ExternalHttpClientWrapper Http { get; set; } = default!;

        private string email = string.Empty;
        private string status = string.Empty;
        private string? lastAssertionJson;

        private async Task RegisterDevice()
        {
            status = string.Empty;
            var begin = await Fido.BeginRegistrationAsync(email);
            if (!begin.Success || begin.Data == null)
            {
                status = "Begin registration failed";
                return;
            }
            var attestation = await JS.InvokeAsync<object>("fido.createCredential", begin.Data);
            var attestationJson = System.Text.Json.JsonSerializer.Serialize(attestation);
            var complete = await Fido.CompleteRegistrationAsync(email, attestationJson);
            status = complete.Success ? "Registered" : "Registration failed";
        }

        private async Task VerifyDevice()
        {
            status = string.Empty;
            var begin = await Fido.BeginAssertionAsync(email);
            if (!begin.Success || begin.Data == null)
            {
                status = "Begin assertion failed";
                return;
            }
            var assertion = await JS.InvokeAsync<object>("fido.getAssertion", begin.Data);
            lastAssertionJson = System.Text.Json.JsonSerializer.Serialize(assertion);
            var complete = await Fido.CompleteAssertionAsync(email, lastAssertionJson);
            status = complete.Success ? "Verified" : "Verification failed";
        }

        private async Task CreateAttendance()
        {
            if (string.IsNullOrEmpty(lastAssertionJson))
            {
                status = "Verify device first";
                return;
            }
            var req = new AttendanceRequestDTO { Email = email, FidoAssertionJson = lastAssertionJson };
            var res = await Http.PostAsJsonAsync<AttendanceRequestDTO, ApiResponse<AttendanceResponseDTO>>("api/Attendance/create", req);
            status = res.Success ? "Attendance created" : $"Attendance failed: {res.Message}";
        }
    }
}
