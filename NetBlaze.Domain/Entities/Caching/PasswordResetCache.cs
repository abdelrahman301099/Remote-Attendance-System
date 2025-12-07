using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetBlaze.Domain.Entities.Caching
{
    public class PasswordResetCache
    {
        public string Code { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int AttemptCount { get; set; } = 0;
    }
}
