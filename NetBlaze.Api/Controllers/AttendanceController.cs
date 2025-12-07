using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Attendance.Request;
using NetBlaze.SharedKernel.Dtos.Attendance.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net;
using System.Threading;

namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class AttendanceController : BaseNetBlazeController
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        [HttpGet("getAttendaces")]
        public IAsyncEnumerable<AttendanceResponseDTO> GetAttendances([FromBody] AttendanceRequestDTO request)
        {
            return _attendanceService.GetListedAttendance(request);
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetAttendanceById([FromRoute] int id,CancellationToken cancellationToken)
        {
            var result = await _attendanceService.GetUserAttendanceByIdAsync(id, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAttendance([FromBody] AttendanceRequestDTO request,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<AttendanceResponseDTO>.ReturnFailureResponse(
                    "Invalid request data", HttpStatusCode.BadRequest));
            }

            var result = await _attendanceService.CreateAttendanceAsync(request, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [HttpDelete("delate")]
        [Authorize(Roles = "Admin,HR")]
        public async Task<IActionResult> DeleteAttendance( [FromRoute] int id,CancellationToken cancellationToken)
        {
            var request = new DeleteAttendanceRequestDTO { Id = id };
            var result = await _attendanceService.DeleteAttendanceAsync(request, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
