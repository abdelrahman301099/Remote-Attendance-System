using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.SharedKernel.Dtos.RandomlyCheck
{
    public class RandomlyCheckResponseDTO
    {
        public int Status { get; set; }
        public string Otp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
        public long UserId { get; set; }

        
    }
}
