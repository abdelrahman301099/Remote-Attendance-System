using Microsoft.EntityFrameworkCore;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Infrastructure.Data.DatabaseContext;
using NetBlaze.Infrastructure.Data.GenericRepository;

namespace NetBlaze.Infrastructure.Data.ParallelService
{
    public class ParallelQueryService : IParallelQueryService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _contextFactory;

        public ParallelQueryService(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> ExecuteAsync<T>(Func<IRepository, Task<T>> operation)
        {
            using var context = _contextFactory.CreateDbContext();

            var repository = new Repository(context);

            return await operation(repository);
        }
    }
}