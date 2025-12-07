using Microsoft.AspNetCore.Identity;

namespace NetBlaze.Domain.Entities.Identity
{
    public class User : IdentityUser<long>
    {
        // Properties
  
        public string DisplayName { get;  set; } = null!;

        public DateTimeOffset CreatedAt { get;  set; } = DateTimeOffset.UtcNow;

        public string? CreatedBy { get;  set; }

        public DateTimeOffset? LastModifiedAt { get;  set; }

        public string? LastModifiedBy { get;  set; }

        public DateTimeOffset? DeletedAt { get;  set; }

        public string? DeletedBy { get;  set; }

        public bool IsActive { get;  set; } = true;

        public bool IsDeleted { get; set; } = false;

        public string? ManagerId { get; set; }

        public User? Manager { get; set; }

        // Navigational Properties
        public long? UserDetailsId { get; set; }

        public virtual UserDetails? UserDetails { get; set; }

        public virtual ICollection<UserRole> UserRoles { get;  set; } = new HashSet<UserRole>();

        public int? DepartmentId { get; set; }

        public virtual Department? Department { get; set; }
    }
}
