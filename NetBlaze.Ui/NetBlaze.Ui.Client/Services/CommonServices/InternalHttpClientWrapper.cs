using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net.Http.Json;

namespace NetBlaze.Ui.Client.Services.CommonServices
{
    public class InternalHttpClientWrapper
    {
        public HttpClient NativeHttpClient { get; }

        public InternalHttpClientWrapper(HttpClient httpClient)
        {
            NativeHttpClient = httpClient;
        }

        public static async Task<TResponse> HandleResponseAsync<TResponse>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<TResponse>(CustomJsonSerializerOptions._jsonSerializerOptions))!;
        }
    }
}
