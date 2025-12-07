using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetBlaze.Domain.Entities;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.Infrastructure.Data.Configurations.MiscConfigurations;
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

       

        protected override void OnModelCreating(ModelBuilder builder)
        {
        
            base.OnModelCreating(builder);

            builder.Entity<User>()
                   .HasOne(u => u.UserDetails)
                   .WithOne(ud => ud.User)
                   .HasForeignKey<UserDetails>(ud => ud.Id); 
        
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.ConfigureIdentityTablesNames();

            builder.SetGlobalIsDeletedFilterToAllEntities();
        }
    }
}