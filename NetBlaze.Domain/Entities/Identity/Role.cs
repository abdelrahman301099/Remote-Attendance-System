using Microsoft.AspNetCore.Identity;

namespace NetBlaze.Domain.Entities.Identity
{
    public class Role : IdentityRole<long>
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

        public ICollection<UserRole> UserRoles { get; private set; } = [];


        // Domain Methods

        public Role() { }

        private Role(string name)
        {
            Name = name.Trim();
            NormalizedName = name.Trim().ToUpper();
        }

        public static Role Create(string name)
        {
            return new Role(name);
        }

        public void Update(string name)
        {
            Name = name.Trim();
            NormalizedName = name.Trim().ToUpper();
        }

        public void SetIsDeletedToTrue() => IsDeleted = true;

        public void ToggleIsActive() => IsActive = !IsActive;
    }
}