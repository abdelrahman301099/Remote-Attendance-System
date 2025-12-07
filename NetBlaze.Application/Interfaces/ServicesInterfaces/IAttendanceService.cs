using NetBlaze.SharedKernel.Dtos.Attendance.Request;
using NetBlaze.SharedKernel.Dtos.Attendance.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Application.Interfaces.ServicesInterfaces
{
   
        public interface IAttendanceService
        {
            IAsyncEnumerable<AttendanceResponseDTO> GetListedAttendance(AttendanceRequestDTO attendanceRequestDto);

            Task<ApiResponse<AttendanceResponseDTO>> CreateAttendanceAsync(AttendanceRequestDTO request,CancellationToken cancellationToken = default);

            Task<ApiResponse<AttendanceResponseDTO>> GetUserAttendanceByIdAsync(int UserId, CancellationToken cancellationToken = default);

            Task<ApiResponse<object>> DeleteAttendanceAsync(DeleteAttendanceRequestDTO request,CancellationToken cancellationToken = default);
        }
    
}
