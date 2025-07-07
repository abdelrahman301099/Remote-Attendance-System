using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Sample.Requests;
using NetBlaze.SharedKernel.Dtos.Sample.Responses;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    public class SampleController : BaseNetBlazeController, ISampleService
    {
        private readonly ISampleService _sampleService;

        public SampleController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [HttpGet("list")]
        public IAsyncEnumerable<GetListedSampleResponseDto> GetListedSample()
        {
            return _sampleService.GetListedSample();
        }

        [HttpGet]
        public async Task<ApiResponse<GetSampleResponseDto>> GetSampleAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _sampleService.GetSampleAsync(id, cancellationToken);
        }

        [HttpPost("add")]
        public async Task<ApiResponse<object>> AddSampleAsync(AddSampleRequestDto addSampleRequestDto, CancellationToken cancellationToken = default)
        {
            return await _sampleService.AddSampleAsync(addSampleRequestDto, cancellationToken);
        }

        [HttpPut("update")]
        public async Task<ApiResponse<object>> UpdateSampleAsync(UpdateSampleRequestDto updateSampleRequestDto, CancellationToken cancellationToken = default)
        {
            return await _sampleService.UpdateSampleAsync(updateSampleRequestDto, cancellationToken);
        }

        [HttpDelete("delete")]
        public async Task<ApiResponse<object>> DeleteSampleAsync(long id, CancellationToken cancellationToken = default)
        {
            return await _sampleService.DeleteSampleAsync(id, cancellationToken);
        }

        [HttpGet("throwException")]
        public Task ThrowInvalidOperationException()
        {
            return _sampleService.ThrowInvalidOperationException();
        }
    }
}
