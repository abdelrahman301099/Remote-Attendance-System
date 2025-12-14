
using NetBlaze.SharedKernel.HelperUtilities.General;
using NetBlaze.Domain.Entities.Views;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
    public interface IReportsService
    {
  
        Task<ApiResponse<List<AttendanceReportDTO>>> GetEmployeeAttendanceAsync(long userId, DateTime from, DateTime to, CancellationToken cancellationToken = default);

        Task<ApiResponse<List<RandomlyCheckReportDTO>>> GetRandomChecksForUserAsync(long userId, DateTime from, DateTime to, CancellationToken cancellationToken = default);
    }
}
