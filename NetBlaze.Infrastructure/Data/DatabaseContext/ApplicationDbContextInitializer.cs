using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetBlaze.Application.Interfaces.General;


namespace NetBlaze.Infrastructure.Data.DatabaseContext
{
    public static class InitializerExtensions
    {
        public static async Task InitializeDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

            await initializer.InitializeAsync();

            await initializer.SeedAsync();
        }
    }

    public class ApplicationDbContextInitializer
    {
        private readonly ILogger<ApplicationDbContextInitializer> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationDbContextInitializer(ILogger<ApplicationDbContextInitializer> logger,
                                               ApplicationDbContext context,
                                               IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();
        }

        public async Task SeedAsync()
        {
            await TrySeedAsync();
        }

        public async Task TrySeedAsync()
        {
            // WARNING: Missing with methods order can lead to errors when seeding the database for the first time.

            await TrySeedSystemPredefinedRolesAsync();

            await TrySeedRootAccountAsync();
        }

        private async Task TrySeedSystemPredefinedRolesAsync()
        {

        }

        private async Task TrySeedRootAccountAsync()
        {

        }
    }
}