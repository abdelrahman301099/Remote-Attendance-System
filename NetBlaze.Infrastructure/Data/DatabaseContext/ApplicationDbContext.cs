using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Views;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.Infrastructure.Data.Configurations.MiscConfigurations;
using NetBlaze.SharedKernel.Dtos.Reports;
using System;
using System.Reflection;

namespace NetBlaze.Infrastructure.Data.DatabaseContext
{
    public class ApplicationDbContext : IdentityDbContext<User,
                                                          Role,
                                                          long,
                                                          IdentityUserClaim<long>,
                                                          UserRole,
                                                          IdentityUserLogin<long>,
                                                          IdentityRoleClaim<long>,
                                                          IdentityUserToken<long>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        //public DbSet<SampleEntity> Samples => Set<SampleEntity>();
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<RandomlyCheck> RandomlyChecks { get; set; }
        public DbSet<CompanyPolicy> CompanyPolicies { get; set; }
        public DbSet<Vacations> Vacations { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<AttendanceReportDTO> AttendanceReportItemDTO {  get; set; }
        public DbSet<RandomlyCheckReportDTO> RandomlyCheckReportDTO { get; set; }
        public DbSet<AppliedPolicy> AppliedPolicies { get; set; }
        public DbSet<UserPolicyDTO> UserPolicyDTO { get; set; }

       

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            base.OnModelCreating(builder);

            builder.Entity<User>()
                   .HasOne(u => u.UserDetails)
                   .WithOne(ud => ud.User)
                   .HasForeignKey<UserDetails>(ud => ud.Id);
            

            builder.Entity<AppliedPolicy>()
                   .HasKey(k => new { k.AttendanceId, k.PolicyId });
           
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.ConfigureIdentityTablesNames();

            builder.SetGlobalIsDeletedFilterToAllEntities();
        }
    }
}