using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Application.Mappings;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public VacationsService(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResponse<IQueryable<VacationResponseDTO>>> GetListedVacations(int pageNumber, int pageSize)
        {
            

            var list = _unitOfWork.Repository.GetQueryable<Vacations>();

            var vacationResponse = list.Select(v => new VacationResponseDTO
            {
                DayName = v.DayName,
                DayDate = v.DayDate,
                VacationDuration = v.VacationDuration,
                Clarification = v.Clarification   
            });

            list.PaginatedListAsync(pageNumber, pageSize);
            
            return ApiResponse<IQueryable< VacationResponseDTO>>.ReturnSuccessResponse(vacationResponse, "Returned Successfully", HttpStatusCode.OK);
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
            );

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

            await _unitOfWork.Repository.AddAsync<Vacations, int>(entity, cancellationToken);

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken);

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
            var target = await _unitOfWork.Repository.GetSingleAsync<Vacations>(false, x => x.Id == request.Id, cancellationToken);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            //TODO:CREATE HELPER FUNCTION TO DO 
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
            var target = await _unitOfWork.Repository.
                GetSingleAsync<Vacations>(false, x => x.Id == request.Id, cancellationToken);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.SampleNotFound, HttpStatusCode.NotFound);
            }

            target.SetIsDeletedToTrue();
            target.ToggleIsActive();

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleDeletedSuccessfully);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.SampleNotModified, HttpStatusCode.NotModified);
        }

        public async Task<ApiResponse<int>> ImportHolidaysFromIcsAsync(string icsUrl, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(icsUrl))
            {
                return ApiResponse<int>.ReturnFailureResponse(Messages.InvalidRequest, HttpStatusCode.BadRequest);
            }

           
                var client = _httpClientFactory.CreateClient();
                using var response = await client.GetAsync(icsUrl, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    return ApiResponse<int>.ReturnFailureResponse(Messages.ErrorOccurredInServer, HttpStatusCode.BadGateway);
                }

                var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                var events = ParseIcs(content);

                var added = 0;
                foreach (var e in events)
                {
                    var exists = await _unitOfWork.Repository.AnyAsync<Vacations>(
                        x => x.DayDate.Date == e.start.Date && x.DayName == e.summary,
                        cancellationToken).ConfigureAwait(false);

                    if (exists)
                    {
                        continue;
                    }

                    var entity = new Vacations
                    {
                        DayName = e.summary,
                        DayDate = e.start.Date,
                        VacationDuration = Math.Max(1, (int)Math.Ceiling((e.end.Date - e.start.Date).TotalDays)),
                        Clarification = e.summary
                    };

                    await _unitOfWork.Repository.AddAsync<Vacations, int>(entity, cancellationToken).ConfigureAwait(false);
                    added++;
                }

                if (added == 0)
                {
                    return ApiResponse<int>.ReturnSuccessResponse(0, Messages.SampleNotModified);
                }

                var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

                return ApiResponse<int>.ReturnSuccessResponse(rows, Messages.SampleAddedSuccessfully);
            
        }

        private static List<(string summary, DateTime start, DateTime end)> ParseIcs(string ics)
        {
            var list = new List<(string summary, DateTime start, DateTime end)>();
            string? summary = null;
            DateTime? dtStart = null;
            DateTime? dtEnd = null;

            using var reader = new StringReader(ics);
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.StartsWith("BEGIN:VEVENT", StringComparison.OrdinalIgnoreCase))
                {
                    summary = null;
                    dtStart = null;
                    dtEnd = null;
                    continue;
                }

                if (line.StartsWith("END:VEVENT", StringComparison.OrdinalIgnoreCase))
                {
                    if (summary != null && dtStart.HasValue)
                    {
                        var start = dtStart.Value;
                        var end = dtEnd ?? start.AddDays(1);
                        list.Add((summary, start, end));
                    }
                    summary = null;
                    dtStart = null;
                    dtEnd = null;
                    continue;
                }

                if (line.StartsWith("SUMMARY:", StringComparison.OrdinalIgnoreCase))
                {
                    summary = line.Substring("SUMMARY:".Length).Trim();
                    continue;
                }

                if (line.StartsWith("DTSTART", StringComparison.OrdinalIgnoreCase))
                {
                    var value = ExtractDateValue(line);
                    dtStart = value;
                    continue;
                }

                if (line.StartsWith("DTEND", StringComparison.OrdinalIgnoreCase))
                {
                    var value = ExtractDateValue(line);
                    dtEnd = value;
                    continue;
                }
            }

            return list;
        }

        private static DateTime ExtractDateValue(string line)
        {
            var idx = line.IndexOf(":", StringComparison.Ordinal);
            var raw = idx >= 0 ? line[(idx + 1)..].Trim() : string.Empty;

            if (DateTime.TryParseExact(raw, "yyyyMMdd", null, System.Globalization.DateTimeStyles.AssumeUniversal, out var dateOnly))
            {
                return dateOnly;
            }

            if (DateTime.TryParseExact(raw, "yyyyMMdd'T'HHmmss'Z'", null, System.Globalization.DateTimeStyles.AdjustToUniversal, out var dateTimeUtc))
            {
                return dateTimeUtc;
            }

            if (DateTime.TryParse(raw, out var general))
            {
                return general;
            }

            return DateTime.UtcNow.Date;
        }

        #region HELPER


        public async Task<ApiResponse<bool>> CheckIfTodayIsVacationAsync(CancellationToken cancellationToken = default)
        {
            var today = DateTime.UtcNow.Date;
           
            var any = await _unitOfWork.Repository
                .AnyAsync<Vacations>(
                x => x.DayDate.Date <= today &&
                today < x.DayDate.AddDays(x.VacationDuration).Date,
                cancellationToken
            );

            return ApiResponse<bool>.ReturnSuccessResponse(any);
        }
        #endregion

       
    }
}
