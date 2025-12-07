using NetBlaze.Domain.Common;
using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Domain.Entities
{
    public class Attendance:BaseEntity<int>
    {
        public DateTime Time { get; set; }

        public DateTime Date { get; set; }

        public int CompanyPolicyId { get; set; }

        public int UserId { get; set; }

        //Navigational
        public virtual CompanyPolicy CompanyPolicy { get; set; }

        public virtual User User { get; set; }
    }
}
