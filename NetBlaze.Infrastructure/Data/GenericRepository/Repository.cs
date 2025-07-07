using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MySqlConnector;
using NetBlaze.Application.Interfaces.General;
using NetBlaze.Domain.Common;
using NetBlaze.Infrastructure.Data.DatabaseContext;
using NetBlaze.SharedKernel.HelperUtilities.Constants;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace NetBlaze.Infrastructure.Data.GenericRepository
{
    /// <summary>
    /// This class contains implementations of repository functions
    /// </summary>
    internal sealed class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">
        /// Database Context <see cref="ApplicationDbContext"/>
        /// </param>
        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        /* DATA MUTATION METHODS */

        public TEntity Add<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Add<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public async Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken).ConfigureAwait(false);
            return entity;
        }

        public IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public IEnumerable<TEntity> AddRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            await _context.Set<TEntity>().AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);
            return entities;
        }

        public void HardDelete<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void HardDelete<TEntity>(object id) where TEntity : class
        {
            var entity = _context.Set<TEntity>().Find(id);
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public void HardDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void HardDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(GenerateExpression<TEntity>(id));
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            _context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task HardDeleteAsync<TEntity>(object id, CancellationToken cancellationToken = default) where TEntity : class
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(GenerateExpression<TEntity>(id), cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public Task HardDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task HardDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(GenerateExpression<TEntity>(id), cancellationToken).ConfigureAwait(false);
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Remove(entity);
        }

        public void SoftDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            entity.SetIsDeletedToTrue();
            Replace<TEntity, TPrimaryKey>(entity);
        }

        public void SoftDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(GenerateExpression<TEntity>(id));
            if (entity == null) return;
            entity.SetIsDeletedToTrue();
            Replace<TEntity, TPrimaryKey>(entity);
        }

        public async Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            entity.SetIsDeletedToTrue();
            await ReplaceAsync<TEntity, TPrimaryKey>(entity, cancellationToken).ConfigureAwait(false);
        }

        public async Task SoftDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(GenerateExpression<TEntity>(id), cancellationToken).ConfigureAwait(false);
            if (entity == null) return;
            entity.SetIsDeletedToTrue();
            await ReplaceAsync<TEntity, TPrimaryKey>(entity, cancellationToken).ConfigureAwait(false);
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public TEntity Update<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().Update(entity);
            return entity;
        }

        public Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            _context.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

        public Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

        public IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        public IEnumerable<TEntity> UpdateRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        public async Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Set<TEntity>().UpdateRange(entities);
            return entities;
        }

        public TEntity Replace<TEntity>(TEntity entity) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public TEntity Replace<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> ReplaceAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        /* DATA QUERY METHODS */

        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class
        {
            return _context.Set<TEntity>().Where(filter);
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).ToList();
        }

        public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).Select(projectExpression).ToList();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).Where(whereExpression).ToList();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking);
            queryable = includeExpression(queryable);
            return [.. queryable];
        }

        public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).Where(whereExpression).Select(projectExpression).ToList();
        }

        public List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return [.. queryable];
        }

        public List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return [.. queryable.Select(projectExpression)];
        }

        public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await FindQueryable<TEntity>(asNoTracking).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await FindQueryable<TEntity>(asNoTracking).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await FindQueryable<TEntity>(asNoTracking).Where(whereExpression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking);
            queryable = includeExpression(queryable);
            return await queryable.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await FindQueryable<TEntity>(asNoTracking).Where(whereExpression).Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return await queryable.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return await queryable.Select(projectExpression).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public IAsyncEnumerable<TProjected> GetMultipleStream<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).Where(whereExpression).Select(projectExpression).AsAsyncEnumerable();
        }

        public TEntity? GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            return queryable.FirstOrDefault();
        }

        public TEntity? GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return queryable.FirstOrDefault();
        }

        public TProjected? GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).Where(whereExpression).Select(projectExpression).FirstOrDefault();
        }

        public TProjected? GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return queryable.Select(projectExpression).FirstOrDefault();
        }

        public async Task<TEntity?> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            return await queryable.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity?> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return await queryable.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjected?> GetSingleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await FindQueryable<TEntity>(asNoTracking).Where(whereExpression).Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjected?> GetSingleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(whereExpression);
            queryable = includeExpression(queryable);
            return await queryable.Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public TEntity? GetById<TEntity>(bool asNoTracking, object id) where TEntity : class
        {
            return FindQueryable<TEntity>(asNoTracking).FirstOrDefault(GenerateExpression<TEntity>(id));
        }

        public TEntity? GetById<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
            queryable = includeExpression(queryable);
            return queryable.FirstOrDefault();
        }

        public TProjected? GetById<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
            return queryable.Select(projectExpression).FirstOrDefault();
        }

        public TProjected? GetById<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
            queryable = includeExpression(queryable);
            return queryable.Select(projectExpression).FirstOrDefault();
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(bool asNoTracking, object id, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await FindQueryable<TEntity>(asNoTracking).FirstOrDefaultAsync(GenerateExpression<TEntity>(id), cancellationToken).ConfigureAwait(false);
        }

        public async Task<TEntity?> GetByIdAsync<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
            queryable = includeExpression(queryable);
            return await queryable.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjected?> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
            return await queryable.Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<TProjected?> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            var queryable = FindQueryable<TEntity>(asNoTracking).Where(GenerateExpression<TEntity>(id));
            queryable = includeExpression(queryable);
            return await queryable.Select(projectExpression).FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);
        }

        public bool Any<TEntity>(Expression<Func<TEntity, bool>> anyExpression) where TEntity : class
        {
            return _context.Set<TEntity>().Any(anyExpression);
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> anyExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            bool result = await _context.Set<TEntity>().AnyAsync(anyExpression, cancellationToken).ConfigureAwait(false);
            return result;
        }

        public int Count<TEntity>() where TEntity : class
        {
            return _context.Set<TEntity>().Count();
        }

        public int Count<TEntity>(Expression<Func<TEntity, bool>> whereExpression) where TEntity : class
        {
            return _context.Set<TEntity>().Where(whereExpression).Count();
        }

        public async Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class
        {
            int count = await _context.Set<TEntity>().CountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class
        {
            int count = await _context.Set<TEntity>().Where(whereExpression).CountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        /* DATA COMMIT METHODS */

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }

        /* MISC METHODS */

        private IQueryable<TEntity> FindQueryable<TEntity>(bool asNoTracking) where TEntity : class
        {
            var queryable = GetQueryable<TEntity>();
            if (asNoTracking)
            {
                queryable = queryable.AsNoTracking();
            }
            return queryable;
        }

        private Expression<Func<TEntity, bool>> GenerateExpression<TEntity>(object id)
        {
            var type = _context.Model.FindEntityType(typeof(TEntity));
            string? pk = type?.FindPrimaryKey()?.Properties.Select(s => s.Name).FirstOrDefault();
            Type? pkType = type?.FindPrimaryKey()?.Properties.Select(p => p.ClrType).FirstOrDefault();

            if (pk == null || pkType == null)
            {
                return Expression.Lambda<Func<TEntity, bool>>(Expression.Constant(false), Expression.Parameter(typeof(TEntity), "entity"));
            }

            object value = Convert.ChangeType(id, pkType, CultureInfo.InvariantCulture);
            ParameterExpression pe = Expression.Parameter(typeof(TEntity), "entity");
            MemberExpression me = Expression.Property(pe, pk);
            ConstantExpression constant = Expression.Constant(value, pkType);
            BinaryExpression body = Expression.Equal(me, constant);
            Expression<Func<TEntity, bool>> expression = Expression.Lambda<Func<TEntity, bool>>(body, pe);
            return expression;
        }

        public async Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedureName, List<(string Name, string Value)> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(storedProcedureName))
                throw new ArgumentException("Stored procedure name cannot be null or empty.", nameof(storedProcedureName));

            if (!Regex.IsMatch(storedProcedureName, RegexTemplate.ValidStoredProcedureName))
                throw new ArgumentException("Invalid stored procedure name.", nameof(storedProcedureName));

            var mySqlParameters = parameters?.Select((p, i) =>
            {
                if (string.IsNullOrWhiteSpace(p.Name) || !Regex.IsMatch(p.Name, RegexTemplate.ValidStoredProcedureParameterName))
                    throw new ArgumentException($"Invalid parameter name at index {i}: {p.Name}");
                return new MySqlParameter($"?{p.Name}", MySqlDbType.VarChar) { Value = p.Value ?? string.Empty };
            }).ToList() ?? [];

            var parameterNames = mySqlParameters.Select(p => p.ParameterName).ToList();

            var commandText = $"CALL {storedProcedureName}({string.Join(", ", parameterNames)})";

            var query = _context.Database.SqlQueryRaw<TResult>(commandText, [.. mySqlParameters]);

            return await query.ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<int> ExecuteUpdateAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperty, CancellationToken cancellationToken = default) where TEntity : class
        {
            return await _context
                .Set<TEntity>()
                .Where(whereExpression)
                .ExecuteUpdateAsync(setProperty, cancellationToken);
        }
    }
}