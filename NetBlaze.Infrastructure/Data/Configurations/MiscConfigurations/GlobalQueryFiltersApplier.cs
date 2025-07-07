using Microsoft.EntityFrameworkCore;
using NetBlaze.Domain.Common;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.Linq.Expressions;

namespace NetBlaze.Infrastructure.Data.Configurations.MiscConfigurations
{
    public static class GlobalQueryFiltersApplier
    {
        public static void SetGlobalIsDeletedFilterToAllEntities(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var isDeletedProperty = entityType.FindProperty(nameof(BaseEntity<long>.IsDeleted));

                if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, MiscConstants.e);

                    var property = Expression.Property(parameter, nameof(BaseEntity<long>.IsDeleted));

                    var filter = Expression.Lambda(Expression.Equal(property, Expression.Constant(false)), parameter);

                    entityType.SetQueryFilter(filter);
                }
            }

            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<UserRole>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<Role>().HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
