using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.Dtos.Department.Request;
using NetBlaze.SharedKernel.Dtos.Department.Response;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IDepartmentService
    {
        IAsyncEnumerable<DepartmentResponseDTO> GetDepartments();
        Task<ApiResponse<DepartmentResponseDTO>> CreateAsync(CreateDepartmentRequestDTO request, CancellationToken cancellationToken = default);
        Task<ApiResponse<object>> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
