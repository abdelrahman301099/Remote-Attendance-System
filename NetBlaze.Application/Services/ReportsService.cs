using Microsoft.EntityFrameworkCore;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Views;
using NetBlaze.SharedKernel.Dtos.RandomlyCheck;
using NetBlaze.SharedKernel.Dtos.Reports;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Services
{
    public class ReportsService : IReportsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<ApiResponse<AttendeesPeriodSummaryDTO>> GetAttendeesDailyAsync(DateTime date, CancellationToken cancellationToken = default)
        //{
        //    var q = _unitOfWork.Repository.GetQueryable<Attendance>()
        //        .Where(x => x.Date.Date == date.Date);
            
        //    var items = await q.Select(x => new AttendanceReportDTO
        //    {
        //        UserId = x.UserId,
        //        UserName = x.User.UserName,
        //        Date = x.Date,
        //        Time = x.Time,
        //        PolicyType = x.CompanyPolicy.PolicyType.ToString(),
        //        PolicyAction = x.CompanyPolicy.Action
        //    }).ToListAsync(cancellationToken).ConfigureAwait(false);

        //    var dto = new AttendeesPeriodSummaryDTO
        //    {
        //        TotalCount = items.Count,
        //        Items = items
        //    };

        //    return ApiResponse<AttendeesPeriodSummaryDTO>.ReturnSuccessResponse(dto);
        //}

        //public async Task<ApiResponse<AttendeesPeriodSummaryDTO>> GetAttendeesMonthlyAsync(int year, int month, CancellationToken cancellationToken = default)
        //{
        //    var q = _unitOfWork.Repository.GetQueryable<Attendance>()
        //        .Where(x => x.Date.Year == year && x.Date.Month == month);

        //    var items = await q.Select(x => new AttendanceReportDTO
        //    {
        //        UserId = x.UserId,
        //        UserName = x.User.UserName,
        //        Date = x.Date,
        //        Time = x.Time,
        //        PolicyType = x.CompanyPolicy.PolicyType.ToString(),
        //        PolicyAction = x.CompanyPolicy.Action
        //    }).ToListAsync(cancellationToken).ConfigureAwait(false);

        //    var dto = new AttendeesPeriodSummaryDTO
        //    {
        //        TotalCount = items.Count,
        //        Items = items
        //    };

        //    return ApiResponse<AttendeesPeriodSummaryDTO>.ReturnSuccessResponse(dto);
        //}

        //public async Task<ApiResponse<AttendeesPeriodSummaryDTO>> GetAttendeesYearlyAsync(int year, CancellationToken cancellationToken = default)
        //{
        //    var q = _unitOfWork.Repository.GetQueryable<Attendance>()
        //        .Where(x => x.Date.Year == year);

        //    var items = await q.Select(x => new AttendanceReportDTO
        //    {
        //        UserId = x.UserId,
        //        UserName = x.User.UserName,
        //        Date = x.Date,
        //        Time = x.Time,
        //        PolicyType = x.CompanyPolicy.PolicyType.ToString(),
        //        PolicyAction = x.CompanyPolicy.Action
        //    }).ToListAsync(cancellationToken).ConfigureAwait(false);

        //    var dto = new AttendeesPeriodSummaryDTO
        //    {
        //        TotalCount = items.Count,
        //        Items = items
        //    };

        //    return ApiResponse<AttendeesPeriodSummaryDTO>.ReturnSuccessResponse(dto);
        //}

        public async Task<ApiResponse<List<AttendanceReportDTO>>> GetEmployeeAttendanceAsync(long userId, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            var uid = (int)userId;
            var q = _unitOfWork.Repository.GetQueryable<Attendance>()
                .Where(x => x.UserId == uid && 
                x.DayDate >= from.Date &&
                x.DayDate <= to.Date);

            var items = await q.Select(x => new AttendanceReportDTO
            {
                UserId = x.UserId,
                UserName = x.User.UserName,
                Date = x.DayDate,
                Time = x.Time,
               
            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ApiResponse<List<AttendanceReportDTO>>.ReturnSuccessResponse(items);
        }



        public async Task<ApiResponse<List<RandomlyCheckReportDTO>>> GetRandomChecksForUserAsync(long userId, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            var q = _unitOfWork.Repository.GetQueryable<RandomlyCheck>()
                .Where(x => x.UserId == userId && x.CreatedAt >= from && x.CreatedAt <= to);

            var items = await q.Select(x => new RandomlyCheckReportDTO
            {
                Status = x.Status,
                OTP = x.Otp,
                CreatedAt = x.CreatedAt,
                ExpiredAt = x.ExpiredAt,
                UserId = x.UserId
            }).ToListAsync(cancellationToken).ConfigureAwait(false);

            return ApiResponse<List<RandomlyCheckReportDTO>>.ReturnSuccessResponse(items);
        }

       

        
         
    }
}
