using NetBlaze.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.CompanyPolicy.Request
{
    public class CompanyPolicyRequestDTO
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public TimeOnly WorkStartTime { get; set; }

        [Required]
        public TimeOnly MaxLate { get; set; }

        [Required]
        public EPolicyType PolicyType { get; set; }

        [Required]
        public int CriticalHours { get; set; }

        [Required]
        public double Action { get; set; }
    }
}
