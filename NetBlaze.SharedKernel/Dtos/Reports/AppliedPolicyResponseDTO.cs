using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.SharedKernel.Dtos.Reports
{
    public class AppliedPolicyResponseDTO
    {
        public long UserId { get; set; }

        public string UserName { get; set; }
        
        public DateTime DayDate { get; set; }

        public string PolicyName { get; set; }

        public TimeOnly EmployeeCheckInAt { get; set; }

        public TimeOnly PolicyMaxLate {  get; set; }

        public double PolicyAction { get; set; }

        
    }
}
