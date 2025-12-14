using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.Domain.Entities.Views;
using NetBlaze.SharedKernel.Dtos.Reports;
using NetBlaze.SharedKernel.HelperUtilities.General;

namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class ReportController : BaseNetBlazeController, IReportsService, IAppliedPolicyService
    {
        private readonly IReportsService _reportsService;
        private readonly IAppliedPolicyService _appliedPolicyService;
        
        public ReportController(IReportsService reportsService, IAppliedPolicyService appliedPolicyService)
        {
            _reportsService = reportsService;
            _appliedPolicyService = appliedPolicyService;
        }

     

        [Authorize(Policy = "CanViewReports")]
        [HttpGet("Get-Attendances")]
        public async Task<ApiResponse<List<AttendanceReportDTO>>> GetEmployeeAttendanceAsync(long userId, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            return await _reportsService.GetEmployeeAttendanceAsync(userId, from, to, cancellationToken);
        }

        [Authorize(Policy = "CanViewReports")]
        [HttpGet("Get-User-RandomChecks")]
        public async Task<ApiResponse<List<RandomlyCheckReportDTO>>> GetRandomChecksForUserAsync(long userId, DateTime from, DateTime to, CancellationToken cancellationToken)
        {
            return await _reportsService.GetRandomChecksForUserAsync(userId, from, to, cancellationToken);
        }

        [Authorize(Policy = "CanViewReports")]
        [HttpPost("Create-Applied-Policy")]
        public Task<ApiResponse<bool>> CreateAppliedPolicy(AppliedPolicyRequestDTO appliedPolicyRequestDTO
                                                            , CancellationToken cancellationToken)
        {
            return _appliedPolicyService.CreateAppliedPolicy(appliedPolicyRequestDTO, cancellationToken);
        }

        [Authorize(Policy = "CanViewReports")]
        [HttpGet("Get-User-Attendance-WithPolicy")]
        public Task<ApiResponse<List<AppliedPolicyResponseDTO>>> GetUserAttendanceReport(long UserId //TODO: DTO
                                                                                        , DateTime from
                                                                                        , DateTime to
                                                                                        , int pageNumber
                                                                                        , int pageSize
                                                                                        , CancellationToken cancellationToken)
        {
            return _appliedPolicyService.GetUserAttendanceReport(UserId, from, to, pageNumber, pageSize, cancellationToken);
        }

       
    }
      
    
}
