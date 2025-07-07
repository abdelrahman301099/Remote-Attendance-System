using NetBlaze.SharedKernel.Dtos.Sample.Requests;
using NetBlaze.SharedKernel.Dtos.Sample.Responses;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.Ui.Client.Services.CommonServices;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;

namespace NetBlaze.Ui.Client.Services
{
    public class BlazSampleService : BaseBlazService
    {
        public BlazSampleService(ExternalHttpClientWrapper externalHttpClientWrapper, CentralizedSnackbarProvider centralizedSnackbarProvider) : base(externalHttpClientWrapper, centralizedSnackbarProvider) { }

        public async IAsyncEnumerable<GetListedSampleResponseDto> GetListedSample([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var response = await _externalHttpClientWrapper.NativeHttpClient.GetAsync(ApiRelativePaths.SAMPLE_LIST, cancellationToken);

            response.EnsureSuccessStatusCode();

            await foreach (var sample in response.Content.ReadFromJsonAsAsyncEnumerable<GetListedSampleResponseDto>(cancellationToken))
            {
                if (sample != null)
                {
                    yield return sample;
                }
            }
        }

        public async Task<ApiResponse<GetSampleResponseDto>> GetSampleAsync(long id, CancellationToken cancellationToken = default)
        {
            var apiResponse = await _externalHttpClientWrapper.GetFromJsonAsync<ApiResponse<GetSampleResponseDto>>($"{ApiRelativePaths.SAMPLE_GET}?{nameof(id)}={id}", cancellationToken);

            return apiResponse;
        }

        public async Task<ApiResponse<object>> AddSampleAsync(AddSampleRequestDto addSampleRequestDto, CancellationToken cancellationToken = default)
        {
            var apiResponse = await _externalHttpClientWrapper.PostAsJsonAsync<AddSampleRequestDto, ApiResponse<object>>(ApiRelativePaths.SAMPLE_ADD, addSampleRequestDto, cancellationToken);

            _centralizedSnackbarProvider.ShowApiResponseSnackbar(apiResponse);

            return apiResponse;
        }

        public async Task<ApiResponse<object>> UpdateSampleAsync(UpdateSampleRequestDto updateSampleRequestDto, CancellationToken cancellationToken = default)
        {
            var apiResponse = await _externalHttpClientWrapper.PutAsJsonAsync<UpdateSampleRequestDto, ApiResponse<object>>(ApiRelativePaths.SAMPLE_UPDATE, updateSampleRequestDto, cancellationToken);

            _centralizedSnackbarProvider.ShowApiResponseSnackbar(apiResponse);

            return apiResponse;
        }

        public async Task<ApiResponse<object>> DeleteSampleAsync(long id, CancellationToken cancellationToken = default)
        {
            var apiResponse = await _externalHttpClientWrapper.DeleteFromJsonAsync<ApiResponse<object>>($"{ApiRelativePaths.SAMPLE_DELETE}?{nameof(id)}={id}", cancellationToken);

            _centralizedSnackbarProvider.ShowApiResponseSnackbar(apiResponse);

            return apiResponse;
        }
    }
}