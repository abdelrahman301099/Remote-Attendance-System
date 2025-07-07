using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Infrastructure.Data.Configurations.EntitiesConfigurations.Identity
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // Properties Configurations

            builder
                .Property(e => e.DisplayName)
                .HasMaxLength(CommonStringLength.LongText)
                .IsRequired();

            builder
                .Property(e => e.UserName)
                .HasMaxLength(CommonStringLength.LongText)
                .IsRequired();

            builder
                .Property(e => e.NormalizedUserName)
                .HasMaxLength(CommonStringLength.LongText)
                .IsRequired();

            builder
                .HasIndex(e => e.NormalizedUserName)
                .IsUnique(false);

            builder
                .Property(e => e.Email)
                .HasMaxLength(CommonStringLength.LongContentText)
                .IsRequired();

            builder
               .HasIndex(e => e.Email)
               .IsUnique();

            builder
                .Property(e => e.NormalizedEmail)
                .HasMaxLength(CommonStringLength.LongContentText)
                .IsRequired();

            builder
                .Property(e => e.PasswordHash)
                .HasMaxLength(CommonStringLength.ExtraLongContentText)
                .IsRequired();

            builder
                .Property(e => e.SecurityStamp)
                .HasMaxLength(CommonStringLength.ExtraLongContentText)
                .IsRequired(false);

            builder
                .Property(e => e.ConcurrencyStamp)
                .HasMaxLength(CommonStringLength.ExtraLongContentText)
                .IsRequired(false);

            builder
                .Property(e => e.PhoneNumber)
                .HasMaxLength(CommonStringLength.ExtendedText)
                .IsRequired();

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
