using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetBlaze.Application.Interfaces.ServicesInterfaces;
using NetBlaze.SharedKernel.Dtos.Department.Request;
using NetBlaze.SharedKernel.Dtos.Department.Response;
using NetBlaze.SharedKernel.HelperUtilities.General;
using System.Net;

namespace NetBlaze.Api.Controllers
{
    [Authorize]
    public class DepartmentController : BaseNetBlazeController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("list")]
        public IAsyncEnumerable<DepartmentResponseDTO> List()
        {
            return _departmentService.GetDepartments();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentRequestDTO request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse<DepartmentResponseDTO>.ReturnFailureResponse("Invalid request data", HttpStatusCode.BadRequest));
            }
            var result = await _departmentService.CreateAsync(request, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            var result = await _departmentService.DeleteAsync(id, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
