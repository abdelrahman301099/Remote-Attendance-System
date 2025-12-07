using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.SharedKernel.Dtos.Vacations.Request;
using NetBlaze.SharedKernel.Dtos.Vacations.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;

namespace NetBlaze.Application.Services
{
    public class VacationsService : IVacationsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VacationsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IAsyncEnumerable<VacationResponseDTO> GetListedVacations()
        {
            return _unitOfWork.Repository.GetMultipleStream<Vacations, VacationResponseDTO>(
                true,
                x => true,
                x => new VacationResponseDTO
                {
                    Id = x.Id,
                    DayName = x.DayName,
                    DayDate = x.DayDate,
                    VacationDuration = x.VacationDuration,
                    Clarification = x.Clarification
                }
            );
        }

        public async Task<ApiResponse<VacationResponseDTO>> GetVacationByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var vacation = await _unitOfWork.Repository.GetByIdAsync<Vacations, VacationResponseDTO>(
                true,
                id,
                x => new VacationResponseDTO
                {
                    Id = x.Id,
                    DayName = x.DayName,
                    DayDate = x.DayDate,
                    VacationDuration = x.VacationDuration,
                    Clarification = x.Clarification
                },
                cancellationToken
            ).ConfigureAwait(false);

            if (vacation == null)
            {
                return ApiResponse<VacationResponseDTO>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            return ApiResponse<VacationResponseDTO>.ReturnSuccessResponse(vacation);
        }

        public async Task<ApiResponse<VacationResponseDTO>> CreateVacationAsync(VacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            var entity = new Vacations
            {
                DayName = request.DayName,
                DayDate = request.DayDate,
                VacationDuration = request.VacationDuration,
                Clarification = request.Clarification ?? string.Empty
            };

            await _unitOfWork.Repository.AddAsync<Vacations, int>(entity, cancellationToken).ConfigureAwait(false);

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                var dto = new VacationResponseDTO
                {
                    Id = entity.Id,
                    DayName = entity.DayName,
                    DayDate = entity.DayDate,
                    VacationDuration = entity.VacationDuration,
                    Clarification = entity.Clarification
                };
                return ApiResponse<VacationResponseDTO>.ReturnSuccessResponse(dto, Messages.SampleAddedSuccessfully);
            }

            return ApiResponse<VacationResponseDTO>.ReturnFailureResponse(Messages.ErrorOccurredInServer, HttpStatusCode.InternalServerError);
        }

        public async Task<ApiResponse<object>> UpdateVacationAsync(UpdateVacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            var target = await _unitOfWork.Repository.GetSingleAsync<Vacations>(false, x => x.Id == request.Id, cancellationToken).ConfigureAwait(false);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            if (!string.IsNullOrWhiteSpace(request.DayName)) target.DayName = request.DayName;
            if (request.DayDate.HasValue) target.DayDate = request.DayDate.Value;
            if (request.VacationDuration.HasValue) target.VacationDuration = request.VacationDuration.Value;
            if (!string.IsNullOrWhiteSpace(request.Clarification)) target.Clarification = request.Clarification!;

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleUpdatedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }

        public async Task<ApiResponse<object>> DeleteVacationAsync(DeleteVacationRequestDTO request, CancellationToken cancellationToken = default)
        {
            var target = await _unitOfWork.Repository.GetSingleAsync<Vacations>(false, x => x.Id == request.Id, cancellationToken).ConfigureAwait(false);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            target.SetIsDeletedToTrue();

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleDeletedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }

        public async Task<ApiResponse<bool>> CheckIfTodayIsVacationAsync(CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;

            var any = await _unitOfWork.Repository.AnyAsync<Vacations>(
                x => x.DayDate.Date <= today && today < x.DayDate.AddDays(x.VacationDuration).Date,
                cancellationToken
            ).ConfigureAwait(false);

            return ApiResponse<bool>.ReturnSuccessResponse(any);
        }
    }
}
