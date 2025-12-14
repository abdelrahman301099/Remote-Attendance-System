using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Infrastructure.Views
{
    public class VwRandomCheckFlatMg
    {

        public static string Up()
        {
            return @"CREATE VIEW vw_random_checks_flat AS
                          SELECT
                           r.Id          AS random_check_id,
                           r.UserId     AS user_id,
                           u.UserName    AS user_name,
                           r.Status      AS status,      
                           r.Otp         AS otp,
                           r.CreatedAt   AS created_at,
                           r.ExpiredAt   AS expired_at
                         FROM RandomlyChecks r
                         JOIN Users u ON u.Id = r.UserId;";
        }

        public static string Down()
        {

            return @"DROP VIEW IF EXISTS vw_random_checks_flat;";
        }
    }
}
