using Microsoft.AspNetCore.Identity;

namespace NetBlaze.Domain.Entities.Identity
{
    public class User : IdentityUser<long>
    {
        // Properties

        public string DisplayName { get; private set; } = null!;

        public DateTimeOffset CreatedAt { get; private set; }

        public string? CreatedBy { get; private set; }

        public DateTimeOffset? LastModifiedAt { get; private set; }

        public string? LastModifiedBy { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public string? DeletedBy { get; private set; }

        public bool IsActive { get; private set; } = true;

        public bool IsDeleted { get; private set; }


        // Navigational Properties

        public ICollection<UserRole> UserRoles { get; private set; } = [];
    }
}
