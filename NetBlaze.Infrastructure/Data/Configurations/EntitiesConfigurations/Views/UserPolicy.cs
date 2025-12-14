using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetBlaze.Domain.Entities.Views;


namespace NetBlaze.Infrastructure.Data.Configurations.EntitiesConfigurations.Views
{
    public class UserPolicy : IEntityTypeConfiguration<UserPolicyDTO>
    {
        public void Configure(EntityTypeBuilder<UserPolicyDTO> builder)
        {
            builder.HasNoKey().ToView("AttendanceWithAppliedPoliciesView");
        }
    }
}
