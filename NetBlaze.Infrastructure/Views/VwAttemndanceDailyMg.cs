using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Infrastructure.Views
{
    public class VwAttemndanceDailyMg
    {
        public static string Up()
        {
            return @"CREATE VIEW vw_attendance_daily_summary AS
                         SELECT DATE(a.Date) AS day_date,
                          COUNT(*)     AS total_attendances
                         FROM Attendances a
                         GROUP BY DATE(a.Date);";
        }

        public static string Down()
        {

            return @"DROP VIEW IF EXISTS vw_attendance_daily_summary; ";
        }
    }
}
