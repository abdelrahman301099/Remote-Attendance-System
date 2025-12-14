
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Vacations.Request;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class VacationsController : BaseNetBlazeController, IVacationsService
    {
        private readonly IVacationsService _vacationsService;

        public VacationsController(IVacationsService vacationsService)
        {
            _vacationsService = vacationsService;
        }

        [HttpGet("Get-Vacations")]
        public async Task<ApiResponse<IQueryable<VacationResponseDTO>>> GetListedVacations(int pageNumber, int pageSizs)
        {
            
            return await _vacationsService.GetListedVacations(pageNumber, pageSizs);
        }


        [Authorize(Policy = "CanManageVacations")]
        [HttpPost("Get-Vacation")]
        public async Task<ApiResponse<VacationResponseDTO>> CreateVacationAsync([FromBody] VacationRequestDTO request, CancellationToken cancellationToken)
        {
            return await _vacationsService.CreateVacationAsync(request, cancellationToken);
        }

        [Authorize(Policy = "CanManageVacations")]
        [HttpPut("Update-Vacation")]
        public async Task<ApiResponse<object>> UpdateVacationAsync([FromBody]UpdateVacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            
            return await _vacationsService.UpdateVacationAsync(request, cancellationToken);
        }

        [Authorize(Policy = "CanManageVacations")]
        [HttpDelete("Delete-Vacation:{id}")]
        public async Task<ApiResponse<object>> DeleteVacationAsync(DeleteVacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            
            return await _vacationsService.DeleteVacationAsync(request, cancellationToken);
        }

        [HttpGet("Is-Today")]
        public async Task<ApiResponse<bool>> CheckIfTodayIsVacationAsync(CancellationToken cancellationToken = default)
        {
            return await _vacationsService.CheckIfTodayIsVacationAsync(cancellationToken);
        }

        [Authorize(Policy = "CanManageVacations")]
        [HttpPost("Import-Holidays")]
        public async Task<ApiResponse<int>> ImportHolidaysFromIcsAsync([FromQuery] string icsUrl, CancellationToken cancellationToken = default)
        {
            return await _vacationsService.ImportHolidaysFromIcsAsync(icsUrl, cancellationToken);
        }

        
    }
}
