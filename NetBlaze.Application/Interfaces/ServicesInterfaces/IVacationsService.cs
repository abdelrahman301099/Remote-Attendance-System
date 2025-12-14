using NetBlaze.SharedKernel.Dtos.Vacations.Request;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IVacationsService
    {
        Task<ApiResponse< IQueryable<VacationResponseDTO>>> GetListedVacations(int pageNumber, int pageSize);

        Task<ApiResponse<VacationResponseDTO>> CreateVacationAsync(VacationRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> UpdateVacationAsync(UpdateVacationRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> DeleteVacationAsync(DeleteVacationRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<bool>> CheckIfTodayIsVacationAsync(CancellationToken cancellationToken = default);

        Task<ApiResponse<int>> ImportHolidaysFromIcsAsync(string icsUrl, CancellationToken cancellationToken = default);
    }
}
