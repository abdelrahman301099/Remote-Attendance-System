

namespace NetBlaze.Domain.Entities.Views
{
    public class RandomlyCheckReportDTO
    {   
        public long UserId { get; set; }

        public string UserName { get; set; } = null!;

        public int Status { get; set; }

        public string OTP { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiredAt { get; set; }
        
    }
}
