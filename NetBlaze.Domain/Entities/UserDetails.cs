using NetBlaze.Domain.Common;
using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Domain.Entities
{
    public class UserDetails:BaseEntity<long>
    {
        public string DeviceNumber { get; set; }

        public string CertificatePassword { get; set; }

        public string Key { get; set; }

        public virtual User User { get; set; }
    }
}
