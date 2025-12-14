

using NetBlaze.SharedKernel.Dtos.Reports;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IAppliedPolicyService
    {
        Task<ApiResponse<bool>> CreateAppliedPolicy(AppliedPolicyRequestDTO appliedPolicyRequestDTO,
                                                    CancellationToken cancellationToken);
        
       Task<ApiResponse<List<AppliedPolicyResponseDTO>>> GetUserAttendanceReport (long UserId,
                                                                                 DateTime from,
                                                                                 DateTime to,
                                                                                 int pageNumber,
                                                                                 int pageSize,
                                                                                 CancellationToken cancellationToken); 

    }
}
