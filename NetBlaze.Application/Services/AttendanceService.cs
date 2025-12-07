using Microsoft.EntityFrameworkCore;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.SharedKernel.Dtos.Attendance.Request;
using NetBlaze.SharedKernel.Dtos.Attendance.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.SharedKernel.SharedResources;
using System.Net;


namespace NetBlaze.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVacationsService _vacationsService;

        public AttendanceService(IUnitOfWork unitOfWork, IVacationsService vacationsService)
        {
            _unitOfWork = unitOfWork;
            _vacationsService = vacationsService;
        }

        public IAsyncEnumerable<AttendanceResponseDTO> GetListedAttendance(AttendanceRequestDTO attendanceRequestDto)
        {
            var listed = _unitOfWork
                .Repository
                .GetMultipleStream<Attendance, AttendanceResponseDTO>(
                    true,
                    x =>
                        (attendanceRequestDto.UserId == null || x.UserId == attendanceRequestDto.UserId.Value) &&
                        (attendanceRequestDto.Username == null || x.User.UserName == attendanceRequestDto.Username) &&
                        (attendanceRequestDto.Email == null || x.User.Email == attendanceRequestDto.Email),
                    x => new AttendanceResponseDTO
                    {
                        UserName = x.User.UserName,
                        Date = x.Date,
                        Time = x.Time,
                        PolicyId = x.CompanyPolicyId
                    }
                );

            return listed;
        }

        public async Task<ApiResponse<AttendanceResponseDTO>> CreateAttendanceAsync( AttendanceRequestDTO request, CancellationToken cancellationToken = default)
        {
            var IsVacation = await _vacationsService.CheckIfTodayIsVacationAsync();

            if (IsVacation.Data)
            {
                return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(
                                                         Messages.TodayIsVacation,
                                                         HttpStatusCode.BadRequest);
            }

            var user = await _unitOfWork.Repository.GetSingleAsync<Attendance>(true, x => x.UserId == request.UserId);
                if (user == null)
                {
                    return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(
                        Messages.UserNotFound, HttpStatusCode.NotFound);
                }

                
                var companyPolicyId = await GetUserPolicyByAttendanceAsync(user.Id, DateTime.Today,cancellationToken);
                if (companyPolicyId == null)
                {
                    return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(
                        Messages.CompanyPolicyNotAssignedToUser, HttpStatusCode.BadRequest);
                }

                var currentDateTime = DateTime.Now;

                var attendance = new Attendance
                {
                    UserId = user.Id,
                    Time = currentDateTime,
                    Date = currentDateTime.Date,
                    CompanyPolicyId = companyPolicyId,
                    CreatedAt = DateTimeOffset.UtcNow
                    
                };

                
                var response = new AttendanceResponseDTO
                {
                    UserName = user.User.UserName,
                    Date = attendance.Date,
                    Time = attendance.Time,
                    PolicyId = companyPolicyId
                };


            await _unitOfWork.Repository.AddAsync<Attendance, int>(attendance, cancellationToken).ConfigureAwait(false);

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<AttendanceResponseDTO>.ReturnSuccessResponse(null, Messages.AttendanceAddedSuccessfully);
            }

            return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(Messages.AttendanceAddFailed);

        }
      
        public async Task<ApiResponse<object>> DeleteAttendanceAsync(DeleteAttendanceRequestDTO deleteAttendanceRequestDto, CancellationToken cancellationToken = default)
        {
            var target = await _unitOfWork
                .Repository
                .GetSingleAsync<Attendance>(false, x => x.Id == deleteAttendanceRequestDto.Id, cancellationToken)
                .ConfigureAwait(false);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse(Messages.AttendanceNotFound, HttpStatusCode.NotFound);
            }

            target.SetIsDeletedToTrue();

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, Messages.AttendanceDeletedSuccessfully, HttpStatusCode.OK);
            }

            return ApiResponse<object>.ReturnSuccessResponse(null, Messages.AttendanceNotModified, HttpStatusCode.NotModified);
        }


        public async Task<ApiResponse<AttendanceResponseDTO>> GetUserAttendanceByIdAsync(int UserId,CancellationToken cancellationToken = default)
        {

            var userAttendance = await _unitOfWork.Repository.GetByIdAsync<Attendance>(true, UserId);
                    
                if (userAttendance == null)
                {
                    return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(
                        Messages.AttendanceNotFound, HttpStatusCode.NotFound);
                }

                var response = new AttendanceResponseDTO
                {
                    UserName = userAttendance.User.UserName,
                    Date = userAttendance.Date,
                    Time = userAttendance.Time,
                    PolicyId = userAttendance.CompanyPolicyId
                };

                return ApiResponse<AttendanceResponseDTO>.ReturnSuccessResponse(response);
           
        }


        //for help
        #region Helper
        public async Task<int> GetUserPolicyByAttendanceAsync(int userId, DateTime date, CancellationToken cancellationToken = default)
        {
            

            var attendances = await _unitOfWork.Repository
                .GetMultipleAsync<Attendance>(
                    true,
                    x => x.UserId == userId && x.Date.Date == date.Date,
                    cancellationToken: cancellationToken
                );

            var first = attendances.OrderBy(x => x.Time).First();


            var last = attendances.OrderBy(x => x.Time).Last();


            var workedHours = (last.Time - first.Time).TotalHours;
            int roundedHours = (int)Math.Round(workedHours);


            var policy = await _unitOfWork.Repository
                .GetSingleAsync<CompanyPolicy>(
                    true,
                    p => p.CriticalHours == roundedHours,
                    cancellationToken: cancellationToken
                );


            return policy.Id;
        }


        private double? CalculatePolicyAction(DateTime checkInTime, CompanyPolicy policy)
        {

            var checkInTimeOnly = checkInTime.TimeOfDay;
            var workStartTimeOnly = policy.WorkStartTime.TimeOfDay;


            var lateBy = checkInTimeOnly - workStartTimeOnly;

            if (lateBy.TotalMinutes <= 0)
            {

                return 0;
            }

            if (lateBy.TotalHours >= policy.CriticalHours)
            {

                return policy.Action;
            }


            var proportionalAction = (lateBy.TotalHours / policy.CriticalHours) * policy.Action;
            return Math.Round(proportionalAction, 2);
        }
        #endregion

    }


}

