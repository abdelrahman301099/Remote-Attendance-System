using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.Attendance.Request
{
    public class DeleteAttendanceRequestDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
