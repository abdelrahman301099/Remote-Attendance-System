using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Vacations.Request;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    public class VacationsController : BaseNetBlazeController, IVacationsService
    {
        private readonly IVacationsService _vacationsService;

        public VacationsController(IVacationsService vacationsService)
        {
            _vacationsService = vacationsService;
        }

        [HttpGet("Get-Vacations")]
        public IEnumerable<VacationResponseDTO> GetListedVacations()
        {
            
            return  _vacationsService.GetListedVacations();
        }


        [HttpPost()]
        public Task<ApiResponse<VacationResponseDTO>> CreateVacationAsync([FromBody] VacationRequestDTO request, CancellationToken cancellationToken)
        {
            return _vacationsService.CreateVacationAsync(request, cancellationToken);
        }

        [HttpPut]
        public Task<ApiResponse<object>> UpdateVacationAsync([FromBody]UpdateVacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            request.Id = request.Id;
            return _vacationsService.UpdateVacationAsync(request, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,HR")]//TODO:make it variable
        public Task<ApiResponse<object>> DeleteVacationAsync(DeleteVacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            
            return _vacationsService.DeleteVacationAsync(request, cancellationToken);
        }

        [HttpGet("is-today")]
        public Task<ApiResponse<bool>> CheckIfTodayIsVacationAsync(CancellationToken cancellationToken = default)
        {
            return _vacationsService.CheckIfTodayIsVacationAsync(cancellationToken);
        }

    }
}
