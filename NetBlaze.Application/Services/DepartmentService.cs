using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.SharedKernel.Dtos.Department.Request;
using NetBlaze.SharedKernel.Dtos.Department.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net;

namespace NetBlaze.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IAsyncEnumerable<DepartmentResponseDTO> GetDepartments()
        {
            return _unitOfWork.Repository.GetMultipleStream<Department, DepartmentResponseDTO>(
                true,
                _ => true,
                x => new DepartmentResponseDTO
                {
                    Id = x.Id,
                    DepartmentName = x.DepartmentName,
                    Description = x.Description
                }
            );
        }

        public async Task<ApiResponse<DepartmentResponseDTO>> CreateAsync(CreateDepartmentRequestDTO request, CancellationToken cancellationToken = default)
        {
            var exists = await _unitOfWork.Repository.AnyAsync<Department>(x => x.DepartmentName == request.DepartmentName, cancellationToken).ConfigureAwait(false);
            if (exists)
            {
                return ApiResponse<DepartmentResponseDTO>.ReturnFailureResponse("Department already exists", HttpStatusCode.Conflict);
            }

            var entity = new Department
            {
                DepartmentName = request.DepartmentName,
                Description = request.Description ?? string.Empty
            };

            await _unitOfWork.Repository.AddAsync<Department, int>(entity, cancellationToken).ConfigureAwait(false);
            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);
            if (rows > 0)
            {
                var dto = new DepartmentResponseDTO { Id = entity.Id, DepartmentName = entity.DepartmentName, Description = entity.Description };
                return ApiResponse<DepartmentResponseDTO>.ReturnSuccessResponse(dto, "Department created");
            }
            return ApiResponse<DepartmentResponseDTO>.ReturnFailureResponse("Department creation failed");
        }

        public async Task<ApiResponse<object>> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await _unitOfWork.Repository.GetByIdAsync<Department>(true, id, cancellationToken).ConfigureAwait(false);
            if (item == null)
            {
                return ApiResponse<object>.ReturnFailureResponse("Department not found", HttpStatusCode.NotFound);
            }

            _unitOfWork.Repository.HardDelete(item);
            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);
            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, "Department deleted");
            }
            return ApiResponse<object>.ReturnFailureResponse("Department delete failed");
        }
    }
}
