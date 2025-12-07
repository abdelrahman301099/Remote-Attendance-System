using NetBlaze.Domain.Common;
using NetBlaze.SharedKernel.Enums;

namespace NetBlaze.Domain.Entities
{
    public class CompanyPolicy:BaseEntity<int>
    {
        public string Name { get; set; }

        public DateTime WorkStartTime { get; set; }

        public DateTime WorkEndTime { get; set; }

       public EPolicyType PolicyType { get; set; }

        public int CriticalHours { get; set; }

        public double Action { get; set; }

        public virtual ICollection<Vacations> Vacations { get; set; }
    }
}
