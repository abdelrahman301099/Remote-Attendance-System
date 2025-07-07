using System.ComponentModel.DataAnnotations;

namespace NetBlaze.Domain.Common
{
    public class BaseEntity<T> : BaseAuditableEntity where T : struct
    {
        // Properties

        [Key]
        public T Id { get; private set; }

        public bool IsActive { get; private set; } = true;

        public bool IsDeleted { get; private set; }


        // Methods

        public void SetIsDeletedToTrue() => IsDeleted = true;

        public void ToggleIsActive() => IsActive = !IsActive;
    }
}