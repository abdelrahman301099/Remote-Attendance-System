using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Infrastructure.Data.Configurations.EntitiesConfigurations.Identity
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Properties Configurations

            builder
                .Property(e => e.CreatedBy)
                .HasMaxLength(CommonStringLength.LongContentText)
                .IsRequired(false);

            builder
                .Property(e => e.LastModifiedBy)
                .HasMaxLength(CommonStringLength.LongContentText)
                .IsRequired(false);

            builder
                .Property(e => e.DeletedBy)
                .HasMaxLength(CommonStringLength.LongContentText)
                .IsRequired(false);


            // Relationships Configurations

            builder
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
