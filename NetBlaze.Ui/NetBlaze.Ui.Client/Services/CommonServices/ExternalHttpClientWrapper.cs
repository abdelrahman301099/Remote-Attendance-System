using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net.Http.Json;

namespace NetBlaze.Ui.Client.Services.CommonServices
{
    public class ExternalHttpClientWrapper
    {
        public HttpClient NativeHttpClient { get; }

        public ExternalHttpClientWrapper(HttpClient httpClient)
        {
            NativeHttpClient = httpClient;
        }

        public async Task<TResponse> GetFromJsonAsync<TResponse>(string url, CancellationToken cancellationToken = default)
        {
            return (await NativeHttpClient.GetFromJsonAsync<TResponse>(url, CustomJsonSerializerOptions._jsonSerializerOptions, cancellationToken))!;
        }

        public async Task<TResponse> PostAsJsonAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken cancellationToken = default)
        {
            var response = await NativeHttpClient.PostAsJsonAsync(url, data, cancellationToken);

            return await HandleResponseAsync<TResponse>(response);
        }

        public async Task<TResponse> PutAsJsonAsync<TRequest, TResponse>(string url, TRequest data, CancellationToken cancellationToken = default)
        {
            var response = await NativeHttpClient.PutAsJsonAsync(url, data, cancellationToken);

            return await HandleResponseAsync<TResponse>(response);
        }

        public async Task<TResponse> DeleteFromJsonAsync<TResponse>(string url, CancellationToken cancellationToken = default)
        {
            var response = await NativeHttpClient.DeleteAsync(url, cancellationToken);

            return await HandleResponseAsync<TResponse>(response);
        }

        public static async Task<TResponse> HandleResponseAsync<TResponse>(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<TResponse>(CustomJsonSerializerOptions._jsonSerializerOptions))!;
        }
    }
}
