
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using NetBlaze.Domain.Entities.Views;
using NetBlaze.SharedKernel.Dtos.Reports;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using NetBlaze.Application.Mappings;


namespace NetBlaze.Application.Services
{
    public class AppliedPolicyService : IAppliedPolicyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AppliedPolicyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task<ApiResponse<bool>> CreateAppliedPolicy(AppliedPolicyRequestDTO appliedPolicyRequestDTO
                                                                , CancellationToken cancellationToken)
        {
            var attendance = await _unitOfWork.Repository.GetSingleAsync<Attendance>(false, a => a.Id == appliedPolicyRequestDTO.AttendanceId);

            var policy = await _unitOfWork.Repository.GetSingleAsync<CompanyPolicy>(false, p => p.Id == appliedPolicyRequestDTO.PolicyId);

            if (appliedPolicyRequestDTO.IsApplied == false)
            {
                return ApiResponse<bool>.ReturnFailureResponse(Messages.InvalidRequest);
            }

            var AppliedPolicyRow = await _unitOfWork.Repository.GetByIdAsync<AppliedPolicy>(
                                             false, appliedPolicyRequestDTO.AttendanceId, cancellationToken);

            var appliedPolicyRecord = new AppliedPolicy { };

            if (AppliedPolicyRow == null) { 
            
             appliedPolicyRecord = new AppliedPolicy
            {
                IsApplied = appliedPolicyRequestDTO.IsApplied,
                DateTime = DateTime.UtcNow,
                UserName = attendance.User.UserName,
                PolicyId = appliedPolicyRequestDTO.PolicyId,
                AttendanceId = appliedPolicyRequestDTO.AttendanceId,
                Action = appliedPolicyRequestDTO.Action,
                PolicyName = policy.Name

            };
                await _unitOfWork.Repository.AddAsync<AppliedPolicy>(appliedPolicyRecord);
            }

            if (AppliedPolicyRow != null)
            {

                 appliedPolicyRecord = new AppliedPolicy
                {
                    IsApplied = appliedPolicyRequestDTO.IsApplied,
                    DateTime = DateTime.UtcNow,
                    UserName = attendance.User.UserName,
                    PolicyId = appliedPolicyRequestDTO.PolicyId,
                    AttendanceId = appliedPolicyRequestDTO.AttendanceId,
                    Action =+ appliedPolicyRequestDTO.Action,
                    PolicyName = policy.Name

                };
                await _unitOfWork.Repository.UpdateAsync<AppliedPolicy>(appliedPolicyRecord);
            }

            var row = await _unitOfWork.Repository.CompleteAsync(cancellationToken);
            if (row > 0)
            {
                return ApiResponse<bool>.ReturnSuccessResponse(true);
            }
            return ApiResponse<bool>.ReturnFailureResponse(Messages.ErrorOccurredInServer);


        }

       

        public async Task<ApiResponse<List<AppliedPolicyResponseDTO>>> GetUserAttendanceReport(long UserId
                                                                                              , DateTime from
                                                                                              , DateTime to
                                                                                              , int pageNumber
                                                                                              , int pageSize
                                                                                              , CancellationToken cancellationToken)
        {
            var user = await _unitOfWork.Repository.GetByIdAsync<User>(true, UserId, cancellationToken);
            if (user == null)
            {
                return ApiResponse<List<AppliedPolicyResponseDTO>>.ReturnFailureResponse(Messages.UserNotFound);
            }

            var q = _unitOfWork.Repository.GetQueryable<UserPolicyDTO>()
                    .Where(
                     x => x.UserId == UserId &&
                     x.DayDate >= from.Date && x.DayDate <= to.Date)
                    .OrderBy(x => x.DayDate).ThenBy(x => x.CheckIn);

            var page = await q.PaginatedListAsync( pageNumber, pageSize);

            if (page == null || page.Items.Count == 0)
            {
                return ApiResponse<List<AppliedPolicyResponseDTO>>.ReturnSuccessResponse(new List<AppliedPolicyResponseDTO>());
            }

            var policyIds = page.Items.Select(r => r.PolicyId).Distinct().ToList();
            var policies = _unitOfWork.Repository.GetMultiple<CompanyPolicy>(true, x => policyIds.Contains(x.Id));
            var policyById = policies.ToDictionary(p => p.Id);

            var items = page.Items.Select(r => new AppliedPolicyResponseDTO
            {
                UserId = UserId,
                UserName = user.UserName,
                DayDate = r.DayDate,
                PolicyName = r.PolicyName,
                EmployeeCheckInAt = r.CheckIn,
                PolicyMaxLate = policyById.TryGetValue(r.PolicyId, out var pol) ? pol.MaxLate : default,
                PolicyAction = r.Action
            }).ToList();

            return ApiResponse<List<AppliedPolicyResponseDTO>>.ReturnSuccessResponse(items);
        }

       
    }
}
