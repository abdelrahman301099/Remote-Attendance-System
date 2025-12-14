using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Infrastructure.Views
{
    public class VwUserPoliciesReportMg
    {
        public static string Up()
        {
            return @"CREATE OR REPLACE VIEW AttendanceWithAppliedPoliciesView AS
SELECT 
    t.UserId,
    t.DayDate,
    t.CheckIn,
    p.Id   AS PolicyId,
    p.Name AS PolicyName,
    p.PolicyType,
    p.Action
FROM
(
    SELECT 
        UserId,
        DayDate,
        MIN(Time) AS CheckIn
    FROM attendances
    GROUP BY UserId, DayDate
) t
LEFT JOIN companypolicies p
    ON t.CheckIn > p.MaxLate;";

        }

        public static string Down()
        {
            return @"DROP VIEW IF EXISTS AttendanceWithAppliedPoliciesView";


        }
    }
}
