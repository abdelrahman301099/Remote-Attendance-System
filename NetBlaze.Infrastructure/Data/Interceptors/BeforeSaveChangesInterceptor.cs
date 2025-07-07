using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetBlaze.Domain.Common;

namespace NetBlaze.Infrastructure.Data.Interceptors
{
    internal class BeforeSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly TimeProvider _dateTime;

        public BeforeSaveChangesInterceptor(TimeProvider dateTime)
        {
            _dateTime = dateTime;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        public void UpdateEntities(DbContext? context)
        {
            if (context == null)
            {
                return;
            }

            foreach (var entry in context.ChangeTracker.Entries())
            {
                var utcNow = _dateTime.GetUtcNow();

                if (entry.State == EntityState.Added)
                {
                    entry.Property(nameof(BaseAuditableEntity.CreatedBy)).CurrentValue = string.Empty; // TODO: Should be changed later
                    entry.Property(nameof(BaseAuditableEntity.CreatedAt)).CurrentValue = utcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property(nameof(BaseAuditableEntity.LastModifiedBy)).CurrentValue = string.Empty; // TODO: Should be changed later
                    entry.Property(nameof(BaseAuditableEntity.LastModifiedAt)).CurrentValue = utcNow;
                }

                var isDeleted = entry.Property(nameof(BaseEntity<ulong>.IsDeleted)).CurrentValue ?? false;

                if ((bool)isDeleted)
                {
                    entry.Property(nameof(BaseAuditableEntity.DeletedBy)).CurrentValue = string.Empty; // TODO: Should be changed later
                    entry.Property(nameof(BaseAuditableEntity.DeletedAt)).CurrentValue = utcNow;
                }
            }
        }
    }
}
