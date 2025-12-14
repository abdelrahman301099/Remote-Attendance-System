using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.CompanyPolicy.Request;
using NetBlaze.SharedKernel.Dtos.CompanyPolicy.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

using Microsoft.AspNetCore.Authorization;
namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class CompanyPolicyController : BaseNetBlazeController, ICompanyPolicyService
    {
        private readonly ICompanyPolicyService _companyPolicyService;
        CompanyPolicyController( ICompanyPolicyService companyPolicyService ) {
        
        _companyPolicyService = companyPolicyService;
        
        }

        [Authorize(Policy = "CanManageCompanyPolicies")]
        [HttpPost]
        public async Task<ApiResponse<CompanyPolicyResponseDTO>> CreateCompanyPolicyAsync(CompanyPolicyRequestDTO request, CancellationToken cancellationToken = default)
        {
            return await _companyPolicyService.CreateCompanyPolicyAsync(request, cancellationToken);
        }

        [Authorize(Policy = "CanManageCompanyPolicies")]
        [HttpDelete]
        public async Task<ApiResponse<object>> DeleteCompanyPolicyAsync(DeleteCompanyPolicyRequestDTO request, CancellationToken cancellationToken = default)
        {
            return await _companyPolicyService.DeleteCompanyPolicyAsync(request, cancellationToken);
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<CompanyPolicyResponseDTO>> GetCompanyPolicyByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _companyPolicyService.GetCompanyPolicyByIdAsync(id, cancellationToken);
        }

        [HttpGet]
        public IAsyncEnumerable<CompanyPolicyResponseDTO> GetListedCompanyPolicies()
        {
            return _companyPolicyService.GetListedCompanyPolicies();
        }

        [Authorize(Policy = "CanManageCompanyPolicies")]
        [HttpPut]
        public async Task<ApiResponse<object>> UpdateCompanyPolicyAsync([FromBody] UpdateCompanyPolicyRequestDTO request, CancellationToken cancellationToken = default)
        {
            return await _companyPolicyService.UpdateCompanyPolicyAsync(request, cancellationToken);
        }
    }
}
