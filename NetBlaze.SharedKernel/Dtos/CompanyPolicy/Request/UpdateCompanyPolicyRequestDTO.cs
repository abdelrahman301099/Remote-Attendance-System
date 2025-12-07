using NetBlaze.SharedKernel.Enums;
using System.ComponentModel.DataAnnotations;

namespace NetBlaze.SharedKernel.Dtos.CompanyPolicy.Request
{
    public class UpdateCompanyPolicyRequestDTO
    {
        [Required]
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? WorkStartTime { get; set; }

        public DateTime? WorkEndTime { get; set; }

        public EPolicyType? PolicyType { get; set; }

        public int? CriticalHours { get; set; }

        public double? Action { get; set; }
    }
}
