using NetBlaze.SharedKernel.Enums;

namespace NetBlaze.SharedKernel.Dtos.CompanyPolicy.Response
{
    public class CompanyPolicyResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public TimeOnly WorkStartTime { get; set; }
        public TimeOnly MaxLate { get; set; }
        public string PolicyType { get; set; } = null!;
        public int CriticalHours { get; set; }
        public double Action { get; set; }
    }
}
