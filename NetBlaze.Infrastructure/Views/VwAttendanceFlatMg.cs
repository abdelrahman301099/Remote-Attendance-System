using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Infrastructure.Views
{
    public class VwAttendanceFlatMg
    {
        public static string Up()
        {
          return @" CREATE VIEW vw_attendance_flat AS
                        SELECT
                         a.Id                  AS attendance_id,
                         a.UserId1             AS user_id,
                         u.UserName            AS user_name,
                         a.Date                AS attendance_date,
                         a.Time                AS attendance_time,
                         a.CompanyPolicyId     AS company_policy_id,
                         cp.Name               AS company_policy_name,
                         cp.PolicyType         AS policy_type,      
                         cp.Action             AS policy_action     
                       FROM Attendances a
                       JOIN Users u            ON u.Id = a.UserId1
                       JOIN CompanyPolicies cp ON cp.Id = a.CompanyPolicyId;";
        }

        public static string Down() {

            return @"DROP VIEW IF EXISTS vw_attendance_flat;";
        }
    }
}
