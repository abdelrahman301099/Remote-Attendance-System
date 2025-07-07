using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlaze.Domain.Entities;
using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Infrastructure.Data.Configurations.EntitiesConfigurations
{
    public class SampleEntityConfiguration : IEntityTypeConfiguration<SampleEntity>
    {
        public void Configure(EntityTypeBuilder<SampleEntity> builder)
        {
            // Properties Configurations

            builder
                .Property(e => e.Name)
                .HasMaxLength(CommonStringLength.StandardText)
                .IsRequired();

            builder
                .Property(e => e.Description)
                .HasMaxLength(CommonStringLength.LongText)
                .IsRequired();
        }
    }
}
