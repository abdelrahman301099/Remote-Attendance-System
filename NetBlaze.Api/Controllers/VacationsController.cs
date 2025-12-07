using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Vacations.Request;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    public class VacationsController : BaseNetBlazeController
    {
        private readonly IVacationsService _vacationsService;

        public VacationsController(IVacationsService vacationsService)
        {
            _vacationsService = vacationsService;
        }

        [HttpGet]
        public async Task<ApiResponse<List<VacationResponseDTO>>> GetAll(CancellationToken cancellationToken)
        {
            var list = new List<VacationResponseDTO>();
            await foreach (var item in _vacationsService.GetListedVacations().WithCancellation(cancellationToken))
            {
                list.Add(item);
            }
            return ApiResponse<List<VacationResponseDTO>>.ReturnSuccessResponse(list);
        }

        [HttpGet("{id:int}")]
        public Task<ApiResponse<VacationResponseDTO>> GetById(int id, CancellationToken cancellationToken)
        {
            return _vacationsService.GetVacationByIdAsync(id, cancellationToken);
        }

        [HttpPost]
        public Task<ApiResponse<VacationResponseDTO>> Create([FromBody] VacationRequestDTO request, CancellationToken cancellationToken)
        {
            return _vacationsService.CreateVacationAsync(request, cancellationToken);
        }

        [HttpPut("{id:int}")]
        public Task<ApiResponse<object>> Update(int id, [FromBody] UpdateVacationRequestDTO request, CancellationToken cancellationToken)
        {
            request.Id = id;
            return _vacationsService.UpdateVacationAsync(request, cancellationToken);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,HR")]
        public Task<ApiResponse<object>> Delete(int id, CancellationToken cancellationToken)
        {
            var request = new DeleteVacationRequestDTO { Id = id };
            return _vacationsService.DeleteVacationAsync(request, cancellationToken);
        }

        [HttpGet("is-today")]
        public Task<ApiResponse<bool>> IsTodayVacation(CancellationToken cancellationToken)
        {
            return _vacationsService.CheckIfTodayIsVacationAsync(cancellationToken);
        }
    }
}
