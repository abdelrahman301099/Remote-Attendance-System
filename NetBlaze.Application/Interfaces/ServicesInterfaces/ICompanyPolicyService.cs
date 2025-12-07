using NetBlaze.SharedKernel.Dtos.CompanyPolicy.Request;
using NetBlaze.SharedKernel.Dtos.CompanyPolicy.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface ICompanyPolicyService
    {
        IAsyncEnumerable<CompanyPolicyResponseDTO> GetListedCompanyPolicies();

        Task<ApiResponse<CompanyPolicyResponseDTO>> GetCompanyPolicyByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<ApiResponse<CompanyPolicyResponseDTO>> CreateCompanyPolicyAsync(CompanyPolicyRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> UpdateCompanyPolicyAsync(UpdateCompanyPolicyRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> DeleteCompanyPolicyAsync(DeleteCompanyPolicyRequestDTO request, CancellationToken cancellationToken = default);
    }
}
