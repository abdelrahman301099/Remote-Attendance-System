using NetBlaze.SharedKernel.Dtos.Sample.Requests;
using NetBlaze.SharedKernel.Dtos.Sample.Responses;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface ISampleService
    {
        IAsyncEnumerable<GetListedSampleResponseDto> GetListedSample();

        Task<ApiResponse<GetSampleResponseDto>> GetSampleAsync(long id, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> AddSampleAsync(AddSampleRequestDto addSampleRequestDto, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> UpdateSampleAsync(UpdateSampleRequestDto updateSampleRequestDto, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> DeleteSampleAsync(long id, CancellationToken cancellationToken = default);

        Task ThrowInvalidOperationException();
    }
}
