using NetBlaze.Domain.Common;
using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Domain.Entities
{
    public class Attendance:BaseEntity<int>
    {
        public TimeOnly Time { get; set; } = new TimeOnly();

        public DateTime DayDate { get; set; } = DateTime.Today;

        public double WorkedHourse { get; set; }

        //public int CompanyPolicyId { get; set; }

        public long UserId { get; set; }

        //Navigational
        //public virtual CompanyPolicy CompanyPolicy { get; set; }

        public virtual User User { get; set; }
    }
}
