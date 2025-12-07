using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.SharedKernel.Dtos.CompanyPolicy.Request;
using NetBlaze.SharedKernel.Dtos.CompanyPolicy.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;

namespace NetBlaze.Application.Services
{
    public class CompanyPolicyService : ICompanyPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyPolicyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IAsyncEnumerable<CompanyPolicyResponseDTO> GetListedCompanyPolicies()
        {
            return _unitOfWork.Repository.GetMultipleStream<CompanyPolicy, CompanyPolicyResponseDTO>(
                true,
                x => true,
                x => new CompanyPolicyResponseDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    WorkStartTime = x.WorkStartTime,
                    WorkEndTime = x.WorkEndTime,
                    PolicyType = x.PolicyType.ToString(),
                    CriticalHours = x.CriticalHours,
                    Action = x.Action
                }
            );
        }

        public async Task<ApiResponse<CompanyPolicyResponseDTO>> GetCompanyPolicyByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var policy = await _unitOfWork.Repository.GetByIdAsync<CompanyPolicy, CompanyPolicyResponseDTO>(
                true,
                id,
               
                x => new CompanyPolicyResponseDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    WorkStartTime = x.WorkStartTime,
                    WorkEndTime = x.WorkEndTime,
                    PolicyType = x.PolicyType.ToString(),
                    CriticalHours = x.CriticalHours,
                    Action = x.Action
                },
                cancellationToken
            ).ConfigureAwait(false);

            if (policy == null)
            {
                return ApiResponse<CompanyPolicyResponseDTO>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            return ApiResponse<CompanyPolicyResponseDTO>.ReturnSuccessResponse(policy);
        }

        public async Task<ApiResponse<CompanyPolicyResponseDTO>> CreateCompanyPolicyAsync(CompanyPolicyRequestDTO request, CancellationToken cancellationToken = default)
        {
            var entity = new CompanyPolicy
            {
                Name = request.Name,
                WorkStartTime = request.WorkStartTime,
                WorkEndTime = request.WorkEndTime,
                PolicyType = request.PolicyType,
                CriticalHours = request.CriticalHours,
                Action = request.Action
            };

            await _unitOfWork.Repository.AddAsync<CompanyPolicy, int>(entity, cancellationToken).ConfigureAwait(false);

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                var dto = new CompanyPolicyResponseDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    WorkStartTime = entity.WorkStartTime,
                    WorkEndTime = entity.WorkEndTime,
                    PolicyType = entity.PolicyType.ToString(),
                    CriticalHours = entity.CriticalHours,
                    Action = entity.Action
                };
                return ApiResponse<CompanyPolicyResponseDTO>.ReturnSuccessResponse(dto, Messages.SampleAddedSuccessfully);
            }

            return ApiResponse<CompanyPolicyResponseDTO>.ReturnFailureResponse(Messages.ErrorOccurredInServer, HttpStatusCode.InternalServerError);
        }

        public async Task<ApiResponse<object>> UpdateCompanyPolicyAsync(UpdateCompanyPolicyRequestDTO request, CancellationToken cancellationToken = default)
        {
            var target = await _unitOfWork.Repository.GetSingleAsync<CompanyPolicy>(false, x => x.Id == request.Id, cancellationToken).ConfigureAwait(false);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(request.Name)) target.Name = request.Name;
            if (request.WorkStartTime.HasValue) target.WorkStartTime = request.WorkStartTime.Value;
            if (request.WorkEndTime.HasValue) target.WorkEndTime = request.WorkEndTime.Value;
            if (request.PolicyType.HasValue) target.PolicyType = request.PolicyType.Value;
            if (request.CriticalHours.HasValue) target.CriticalHours = request.CriticalHours.Value;
            if (request.Action.HasValue) target.Action = request.Action.Value;

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleUpdatedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }

        public async Task<ApiResponse<object>> DeleteCompanyPolicyAsync(DeleteCompanyPolicyRequestDTO request, CancellationToken cancellationToken = default)
        {
            var target = await _unitOfWork.Repository.GetSingleAsync<CompanyPolicy>(false, x => x.Id == request.Id, cancellationToken).ConfigureAwait(false);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            target.SetIsDeletedToTrue();

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleDeletedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }
    }
}
