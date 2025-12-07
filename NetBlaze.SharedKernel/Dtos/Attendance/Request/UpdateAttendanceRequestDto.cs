using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.Attendance.Request
{
    public class UpdateAttendanceRequestDTO
    {
        [Required]
        public int Id { get; set; }

        public DateTime? Time { get; set; }

        public DateTime? Date { get; set; }

        public int? CompanyPolicyId { get; set; }
    }
}
