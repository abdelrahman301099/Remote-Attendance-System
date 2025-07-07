using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Domain.Entities.Identity;
using NetBlaze.Infrastructure.Data.DatabaseContext;
using NetBlaze.Infrastructure.Data.GenericRepository;
using NetBlaze.Infrastructure.Data.Interceptors;
using NetBlaze.Infrastructure.Data.ParallelService;
using NetBlaze.Infrastructure.Data.UnitOfWork;
using NetBlaze.Infrastructure.GenericMemoryCacheRepository;
using NetBlaze.Infrastructure.InfraServices;
using NetBlaze.SharedKernel.HelperUtilities.Constants;

namespace NetBlaze.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString(MiscConstants.Default);

            builder.Services.AddScoped<ISaveChangesInterceptor, BeforeSaveChangesInterceptor>();

            builder.Services.AddDbContext<ApplicationDbContext>((sp, opt) =>
            {
                opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                opt.EnableThreadSafetyChecks(true);
            });

            builder.Services.AddDbContextFactory<ApplicationDbContext>((sp, opt) =>
            {
                opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                opt.EnableThreadSafetyChecks(true);
            }, ServiceLifetime.Scoped);

            builder.Services.AddScoped<ApplicationDbContextInitializer>();

            builder.Services.AddHttpClient();

            builder
                .Services
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSingleton(TimeProvider.System);

            builder.Services.AddScoped<IRepository, Repository>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IJwtBearerService, JwtBearerService>();

            builder.Services.AddScoped<IParallelQueryService, ParallelQueryService>();

            builder.Services.AddMemoryCache();

            builder.Services.AddSingleton<IMemoryCacheRepository, MemoryCacheRepository>();

            builder.AddBearerAuthenticationService();

            builder.Services.AddAuthorization();
        }
    }
}