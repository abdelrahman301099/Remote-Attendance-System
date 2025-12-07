using NetBlaze.SharedKernel.Dtos.Vacations.Request;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IVacationsService
    {
        IAsyncEnumerable<VacationResponseDTO> GetListedVacations();

        Task<ApiResponse<VacationResponseDTO>> GetVacationByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<ApiResponse<VacationResponseDTO>> CreateVacationAsync(VacationRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> UpdateVacationAsync(UpdateVacationRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<object>> DeleteVacationAsync(DeleteVacationRequestDTO request, CancellationToken cancellationToken = default);

        Task<ApiResponse<bool>> CheckIfTodayIsVacationAsync(CancellationToken cancellationToken = default);
    }
}
