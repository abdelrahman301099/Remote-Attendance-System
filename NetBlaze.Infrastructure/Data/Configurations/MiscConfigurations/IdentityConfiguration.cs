using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetBlaze.Domain.Entities.Identity;

namespace NetBlaze.Infrastructure.Data.Configurations.MiscConfigurations
{
    public static class IdentityConfiguration
    {
        public static void ConfigureIdentityTablesNames(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity => entity.ToTable("Users"));

            modelBuilder.Entity<Role>(entity => entity.ToTable("Roles"));

            modelBuilder.Entity<UserRole>(entity => entity.ToTable("UserRoles"));


            // Skip these tables from being included in the migrations and prevent them from being created in the database.

            modelBuilder.Ignore<IdentityRoleClaim<long>>();

            modelBuilder.Ignore<IdentityUserClaim<long>>();

            modelBuilder.Ignore<IdentityUserLogin<long>>();

            modelBuilder.Ignore<IdentityUserToken<long>>();
        }
    }
}
