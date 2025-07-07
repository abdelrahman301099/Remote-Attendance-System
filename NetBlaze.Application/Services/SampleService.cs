using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.SharedKernel.Dtos.Sample.Requests;
using NetBlaze.SharedKernel.Dtos.Sample.Responses;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;

namespace NetBlaze.Application.Services
{
    public class SampleService : ISampleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SampleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async IAsyncEnumerable<GetListedSampleResponseDto> GetListedSample()
        {
            var listedSamples = _unitOfWork
                .Repository
                .GetMultipleStream<SampleEntity, GetListedSampleResponseDto>(
                    true,
                    _ => true,
                    x => new GetListedSampleResponseDto(x.Id, x.Name, x.Status)
                );

            await foreach (var sample in listedSamples)
            {
                yield return sample;
            }
        }

        public async Task<ApiResponse<GetSampleResponseDto>> GetSampleAsync(long id, CancellationToken cancellationToken = default)
        {
            var sampleDto = await _unitOfWork
                .Repository
                .GetSingleAsync<SampleEntity, GetSampleResponseDto>(
                    true,
                    x => x.Id == id,
                    x => new GetSampleResponseDto(
                        x.Id,
                        x.IntegerValue,
                        x.DecimalValue,
                        x.Name,
                        x.Description,
                        x.StartDate,
                        x.UniqueIdentifier,
                        x.Status),
                    cancellationToken
                );

            if (sampleDto != null)
            {
                return ApiResponse<GetSampleResponseDto>.ReturnSuccessResponse(sampleDto);
            }

            return ApiResponse<GetSampleResponseDto>.ReturnFailureResponse(Messages.NoSampleFound, HttpStatusCode.NotFound);
        }

        public async Task<ApiResponse<object>> AddSampleAsync(AddSampleRequestDto addSampleRequestDto, CancellationToken cancellationToken = default)
        {
            var sample = SampleEntity.Create(addSampleRequestDto);

            await _unitOfWork.Repository.AddAsync(sample, cancellationToken);

            await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleAddedSuccessfully);
        }

        public async Task<ApiResponse<object>> UpdateSampleAsync(UpdateSampleRequestDto updateSampleRequestDto, CancellationToken cancellationToken = default)
        {
            var targetSample = await _unitOfWork
                .Repository
                .GetSingleAsync<SampleEntity>(
                    false,
                    x => x.Id == updateSampleRequestDto.Id,
                    cancellationToken
                );

            if (targetSample == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            targetSample.Update(updateSampleRequestDto);

            var rowsAffected = await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            if (rowsAffected > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleUpdatedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }

        public async Task<ApiResponse<object>> DeleteSampleAsync(long id, CancellationToken cancellationToken = default)
        {
            var targetSample = await _unitOfWork
                .Repository
                .GetSingleAsync<SampleEntity>(
                    false,
                    x => x.Id == id,
                    cancellationToken
                );

            if (targetSample == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            targetSample.SetIsDeletedToTrue();

            var rowsAffected = await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            if (rowsAffected > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleDeletedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }

        public Task ThrowInvalidOperationException()
        {
            throw new InvalidOperationException();
        }


        #region Helper Methods

        #endregion
    }
}