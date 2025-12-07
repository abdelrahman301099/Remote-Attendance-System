using NetBlaze.SharedKernel.Enums;

namespace NetBlaze.SharedKernel.Dtos.Attendance.Request
{
    public class AttendanceResponseDTO
    {
        public string UserName { get; set; } = null!;

        public DateTime Date { get; set; } = DateTime.MinValue;

        public DateTime Time { get; set; } 

        public int PolicyId { get; set; }

        


    }
}
