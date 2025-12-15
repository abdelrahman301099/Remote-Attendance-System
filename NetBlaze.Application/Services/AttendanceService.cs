using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Identity;
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
        private readonly UserManager<User> _userManager;
        private readonly IFido2Service _fido2Service;

        public AttendanceService(IUnitOfWork unitOfWork, IVacationsService vacationsService, UserManager<User> userManager, IFido2Service fido2Service)
        {
            _unitOfWork = unitOfWork;
            _vacationsService = vacationsService;
            _userManager = userManager;
            _fido2Service = fido2Service;
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
                        Date = x.DayDate,
                        Time = x.Time
                       
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
            
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                {
                    return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(
                        Messages.UserNotFound, HttpStatusCode.NotFound);
                }
            if (string.IsNullOrEmpty(request.FidoAssertionJson))
            {
                return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse("Missing FIDO2 assertion", HttpStatusCode.BadRequest);
            }
            var fidoResult = await _fido2Service.ValidateAssertionForUserAsync(user.Email!, request.FidoAssertionJson, cancellationToken);
            if (!fidoResult.Success || fidoResult.Data == false)
            {
                return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse("FIDO2 validation failed", HttpStatusCode.Forbidden);
            }
            
                
                var companyPolicyId = await GetUserPolicyByAttendanceAsync(user.Id, DateTime.Today,cancellationToken);

                
                

                var attendance = new Attendance
                {
                    UserId = user.Id,
                    DayDate = DateTime.Today,
                    Time = TimeOnly.FromDateTime(DateTime.Now)
                };

                var response = new AttendanceResponseDTO
                {
                    UserName = user.UserName,
                    Date = attendance.DayDate,
                    Time = attendance.Time,
                };


            await _unitOfWork.Repository.AddAsync<Attendance, int>(attendance, cancellationToken).ConfigureAwait(false);

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<AttendanceResponseDTO>.ReturnSuccessResponse(null, "Attendance added successfully");
            }

            return ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse("Attendance add failed");

        }
      
        public async Task<ApiResponse<object>> DeleteAttendanceAsync(DeleteAttendanceRequestDTO deleteAttendanceRequestDto, CancellationToken cancellationToken = default)
        {
            var target = await _unitOfWork
                .Repository
                .GetSingleAsync<Attendance>(false, x => x.Id == deleteAttendanceRequestDto.Id, cancellationToken)
                .ConfigureAwait(false);

            if (target == null)
            {
                return ApiResponse<object>.ReturnFailureResponse("Attendance not found", HttpStatusCode.NotFound);
            }

            target.SetIsDeletedToTrue();

            var rows = await _unitOfWork.Repository.CompleteAsync(cancellationToken).ConfigureAwait(false);

            if (rows > 0)
            {
                return ApiResponse<object>.ReturnSuccessResponse(null, "Attendance deleted successfully");
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

                //var PolicyId = await GetUserPolicyByAttendanceAsync(Userid, DateTime.Today, cancellationToken);

                var response = new AttendanceResponseDTO
                {
                    UserName = userAttendance.User.UserName,
                    Date = userAttendance.DayDate,
                    Time = userAttendance.Time,
                };

                return ApiResponse<AttendanceResponseDTO>.ReturnSuccessResponse(response);
           
        }

       
        //for help
        #region Helper
        public async Task<int> GetUserPolicyByAttendanceAsync(long userId, DateTime date, CancellationToken cancellationToken = default)
        {
            
           
            var attendances = await _unitOfWork.Repository
                .GetMultipleAsync<Attendance>(
                    true,
                    x => x.UserId == userId && x.DayDate == date.Date,
                    cancellationToken: cancellationToken
                );

          
            if (attendances == null || !attendances.Any())
                return 1;

           
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


        private double? CalculatePolicyAction(TimeOnly checkInTime, CompanyPolicy policy)
        {
            var allowedStart = policy.WorkStartTime.Add(policy.MaxLate.ToTimeSpan());
            var lateBy = checkInTime - allowedStart;

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

        private double EvaluateCheckOutPolicy(TimeOnly checkOut, CompanyPolicy policy)
        {
            var expectedEnd = policy.WorkStartTime.Add(TimeSpan.FromHours(policy.CriticalHours));
            var earlyBy = expectedEnd - checkOut;

            if (earlyBy.TotalMinutes <= 0)
            {
                return 0;
            }

            if (earlyBy.TotalHours >= policy.CriticalHours)
            {
                return policy.Action;
            }

            var proportional = (earlyBy.TotalHours / policy.CriticalHours) * policy.Action;
            return Math.Round(proportional, 2);
        }
        #endregion

    }


}

