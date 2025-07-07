using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Infrastructure.Data.Configurations.EntitiesConfigurations.Identity
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Properties Configurations

            builder
                .Property(e => e.Name)
                .HasMaxLength(CommonStringLength.ExtendedText)
                .IsRequired();

            builder
                .Property(e => e.NormalizedName)
                .HasMaxLength(CommonStringLength.ExtendedText)
                .IsRequired();

            builder
                .HasIndex(e => e.NormalizedName)
                .IsUnique(false);

            builder
                .Property(e => e.ConcurrencyStamp)
                .HasMaxLength(CommonStringLength.LongText)
                .IsRequired(false);

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
        }
    }
}
