namespace NetBlaze.Application.Interfaces.General
{
    public interface IParallelQueryService
    {
        Task<T> ExecuteAsync<T>(Func<IRepository, Task<T>> operation);
    }
}