using NetBlaze.SharedKernel.Enums;

namespace NetBlaze.SharedKernel.Dtos.CompanyPolicy.Response
{
    public class CompanyPolicyResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime WorkStartTime { get; set; }
        public DateTime WorkEndTime { get; set; }
        public string PolicyType { get; set; } = null!;
        public int CriticalHours { get; set; }
        public double Action { get; set; }
    }
}
