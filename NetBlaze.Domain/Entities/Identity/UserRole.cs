using Microsoft.AspNetCore.Identity;

namespace NetBlaze.Domain.Entities.Identity
{
    public class UserRole : IdentityUserRole<long>
    {
        // Properties

        public DateTimeOffset CreatedAt { get; private set; }

        public string? CreatedBy { get; private set; }

        public DateTimeOffset? LastModifiedAt { get; private set; }

        public string? LastModifiedBy { get; private set; }

        public DateTimeOffset? DeletedAt { get; private set; }

        public string? DeletedBy { get; private set; }

        public bool IsActive { get; private set; } = true;

        public bool IsDeleted { get; private set; }


        // Navigational Properties

        public User User { get; private set; } = null!;

        public Role Role { get; private set; } = null!;


        // Domain Methods

        public UserRole() { }

        private UserRole(long roleId)
        {
            RoleId = roleId;
        }

        public static UserRole Create(long roleId)
        {
            return new UserRole(roleId);
        }

        public void SetIsDeletedToTrue() => IsDeleted = true;

        public void ToggleIsActive() => IsActive = !IsActive;
    }
}
