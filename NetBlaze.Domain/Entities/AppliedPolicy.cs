using NetBlaze.Domain.Common;
using NetBlaze.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Domain.Entities
{
    public class AppliedPolicy: BaseEntity<int>
    {
        public bool IsApplied { get; set; }

        public DateTime DateTime { get; set; } = DateTime.Now;

        public double Action { get; set; }

        public string UserName { get; set; }

        public int AttendanceId { get; set; }

        public virtual Attendance Attendance { get; set; }

        public string PolicyName { get; set; }

        public int PolicyId { get; set; }

        public virtual CompanyPolicy CompanyPolicy { get; set; }


    }
}
