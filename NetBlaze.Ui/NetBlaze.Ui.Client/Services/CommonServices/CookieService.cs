using Microsoft.JSInterop;
using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Ui.Client.Services.CommonServices
{
    public class CookieService
    {
        private readonly IJSRuntime _jsRuntime;

        public CookieService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task<bool> SetCookieAsync(string name, string value, int days = 7)
        {
            return await _jsRuntime.InvokeAsync<bool>(JavaScriptMethodName.SetCookie, name, value, days);
        }

        public async Task<string> GetCookieAsync(string name)
        {
            return await _jsRuntime.InvokeAsync<string>(JavaScriptMethodName.GetCookie, name);
        }

        public async Task<bool> DeleteCookieAsync(string name)
        {
            return await _jsRuntime.InvokeAsync<bool>(JavaScriptMethodName.DeleteCookie, name);
        }

        public async Task<Dictionary<string, string>> GetAllCookiesAsync()
        {
            return await _jsRuntime.InvokeAsync<Dictionary<string, string>>(JavaScriptMethodName.GetAllCookies);
        }
    }
}
