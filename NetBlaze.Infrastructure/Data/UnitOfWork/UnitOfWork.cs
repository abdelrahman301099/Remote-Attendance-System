using NetBlaze.Application.Interfaces.General;

namespace NetBlaze.Infrastructure.Data.UnitOfWork
{
    /// <summary>
    /// Implementation of Unit-Of-Work pattern
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository repository)
        {
            Repository = repository;
        }

        public IRepository Repository { get; }
    }
}
