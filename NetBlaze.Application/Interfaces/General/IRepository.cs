using Microsoft.EntityFrameworkCore.Query;
using NetBlaze.Domain.Common;
using System.Linq.Expressions;

namespace NetBlaze.Application.Interfaces.General
{
    /// <summary>
    /// This interface implemented base database operation with generic repository pattern
    /// </summary>
    public interface IRepository
    {
        /* DATA MUTATION METHODS */

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs entity insert operation. In additional this methods returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity <see cref="{TEntity}"/>
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Add<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="BaseEntity{TPrimaryKey}"/> and performs entity insert operation. In additional this methods returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of entity primary key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Add<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs entity insert async. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity <see cref="{TEntity}"/>
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{TEntity}"/>
        /// </returns>
        Task<TEntity> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="BaseEntity{TPrimaryKey}"/> and performs entity insert operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of entity primary key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns></returns>
        Task<TEntity> AddAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This methods takes <see cref="IEnumerable{TEntity}"/> and performs entity insert range operation. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="entities">
        /// The entity to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="IEnumerable{TEntity}"/>
        /// </returns>
        IEnumerable<TEntity> AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="IEnumerable{BaseEntity{TPrimaryKey}}"/> and performs entity insert range operation. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be added
        /// </param>
        /// <returns>
        /// Returns <see cref="IEnumerable{TEntity}"/>
        /// </returns>
        IEnumerable<TEntity> AddRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and performs entity insert range operation async version. In additional this methods returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{IEnumerable{TEntity}}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> AddRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="IEnumerable{BaseEntity{TPrimaryKey}}"/> and performs entity insert range operation. In additional this methods returns <see cref="Task{IEnumerable{TEntity}}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of primary key
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be added
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{IEnumerable{TEntity}}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> AddRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs entity hard delete operation.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        void HardDelete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="object"/> and performs entity hard delete operation
        /// </summary>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void HardDelete<TEntity>(object id) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs hard delete operation
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key for Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        void HardDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and performs hard delete operation by id.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void HardDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/>. This method performs entity hard delete operation. In additional this methods returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="object"/> an <see cref="CancellationToken"/>. This method performs hard delete operation async version. In additional returns <see cref="Task"/>
        /// </summary>
        /// <param name="id">
        /// Pk of Entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity>(object id, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and performs hard delete operation async version for BaseEntity. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and <see cref="CancellationToken"/>. This method performs hard delete operation by id async version. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task HardDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and performs soft delete operation
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void SoftDelete<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and performs soft delete operation by id.
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        void SoftDelete<TEntity, TPrimaryKey>(TPrimaryKey id) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> performs soft delete operation. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be deleted
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task SoftDeleteAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TPrimaryKey}"/> and <see cref="CancellationToken"/>. This method performs soft delete operation by id async version. In additional this method returns <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="id">
        /// PK of Entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task SoftDeleteAsync<TEntity, TPrimaryKey>(TPrimaryKey id, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> performs update operation. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        TEntity Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update operation for BaseEntity. In additional this method returs <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Update<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/> performs update operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task<TEntity> UpdateAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update operation for BaseEntity. In additional this method returs <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="Task{TEntity}"/>
        /// </returns>
        Task<TEntity> UpdateAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> performs update operation. In additional returns <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        IEnumerable<TEntity> UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update range operation for BaseEntity. In additional this method returs <see cref="IEnumerable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        IEnumerable<TEntity> UpdateRange<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="CancellationToken"/> performs update operation async version. In additional this methods returns <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task{TResult}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="IEnumerable{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs update operation for BaseEntity. In additional this method returs <see cref="Task{TResult}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entities">
        /// The entities to be updated
        /// </param>
        /// <returns>
        /// Returns <see cref="Task{TResult}"/>
        /// </returns>
        Task<IEnumerable<TEntity>> UpdateRangeAsync<TEntity, TPrimaryKey>(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> performs replace operation. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        TEntity Replace<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs replace operation for BaseEntity. In additional this method returs <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity Replace<TEntity, TPrimaryKey>(TEntity entity) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="CancellationToken"/> performs replace operation async version. In additional this methods returns <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="Task"/>
        /// </returns>
        Task<TEntity> ReplaceAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="{TEntity}"/> and <see cref="{TPrimaryKey}"/>. This method performs replace operation for BaseEntity. In additional this method returs <see cref="Task{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TPrimaryKey">
        /// Type of Primary Key
        /// </typeparam>
        /// <param name="entity">
        /// The entity to be replaced
        /// </param>
        /// <returns>
        /// Returns <see cref="Task{TEntity}"/>
        /// </returns puseudopodia>
        Task<TEntity> ReplaceAsync<TEntity, TPrimaryKey>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : BaseEntity<TPrimaryKey> where TPrimaryKey : struct;

        /* DATA QUERY METHODS */

        /// <summary>
        /// This method provides entity queryable version. In additional this method returns <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <returns>
        /// Returns <see cref="IQueryable{TEntity}"/>
        /// </returns>
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{TDelegate}"/> and apply filter to data source. In additional returns <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="filter">
        /// The filter to apply on the Entity.
        /// </param>
        /// <returns>
        /// Returns <see cref="IQueryable{TEntity}"/>
        /// </returns>
        IQueryable<TEntity> GetQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : class;

        /// <summary>
        /// This method  returns List of Entity without filter. <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking) where TEntity : class;

        /// <summary>
        /// This method provides without filter get all entity but you can convert it to any object you want.
        /// In additional this method takes <see cref="Expression{Func}"/> returns <see cref="List{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> performs apply filter get all entity. In additional returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression see <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/>. This method performs get all with includable entities. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="includeExpression">
        /// <see cref="IIncludableQueryable{TEntity, TProperty}"/> expression
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> where expression and <see cref="Expression{Func}"/> select expression. This method performs apply filter and convert returns get all entity. In additional returns <see cref="List{TPrtojected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of Projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression
        /// </param>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/>, <see cref="Expression{Func}"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/>. This method perform get all entities with filter and includable entities. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        List<TEntity> GetMultiple<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression and <see cref="Expression{Func}"/> select expression. This method perform get all projected object with filter and include entities. In additional returns <see cref="List{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Select expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        List<TProjected> GetMultiple<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method  returns List of Entity without filter async version. <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> and <see cref="CancellationToken"/>. This method performs without filter get all entity but you can convert it to any object you want
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> and <see cref="CancellationToken"/>. This method performs apply filter get all entity. In additional returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/>. This method performs get all with includable entities async version. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="includeExpression">
        /// <see cref="IIncludableQueryable{TEntity, TProperty}"/> expression
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> where expression and <see cref="Expression{Func}"/> select expression. This method performs apply filter async version and convert returns get all entity. In additional returns <see cref="List{TPrtojected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of Projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression
        /// </param>
        /// <param name="projectExpression">
        /// Select expression
        /// </param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/>, <see cref="Expression{Func}"/>, <see cref="IIncludableQueryable{TEntity, TProperty}"/> and <see cref="CancellationToken"/>. This method perform get all entities with filter and includable entities async version. In additional this method returns <see cref="List{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TEntity}"/>
        /// </returns>
        Task<List<TEntity>> GetMultipleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression, <see cref="Expression{Func}"/> select expression and <see cref="CancellationToken"/> cancellation token. This method perform get all projected object with filter and include entities async version. In additional returns <see cref="List{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of Entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where Expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Select expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="List{TProjected}"/>
        /// </returns>
        Task<List<TProjected>> GetMultipleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// Retrieves a filtered and projected stream of entities as an <see cref="IAsyncEnumerable{TProjected}"/>.
        /// Applies the given filtering and projection expressions to entities of type <typeparamref name="TEntity"/>.
        /// This method does not execute the query immediately but returns an asynchronous stream for deferred execution.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to query.</typeparam>
        /// <typeparam name="TProjected">The type of the projected result.</typeparam>
        /// <param name="asNoTracking">Indicates whether to disable EF Core change tracking. Default is <c>false</c>.</param>
        /// <param name="whereExpression">A LINQ expression to filter the entities.</param>
        /// <param name="projectExpression">A LINQ expression to project the filtered entities to the result type.</param>
        /// <returns>
        /// An <see cref="IAsyncEnumerable{TProjected}"/> that represents the filtered and projected query results.
        /// </returns>
        IAsyncEnumerable<TProjected> GetMultipleStream<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking and <see cref="Expression{Func}"/> where expression. This method performs get entity with apply filter. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity? GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression. This method performs get entity with apply filter and includable entities. In additional this method returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity? GetSingle<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression and <see cref="Expression{Func}"/> project the expression. This method performs get projected object with apply filter. In additional returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        TProjected? GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="Expression{Func}"/> project expression and <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression. This method performs get projected object with apply filter and includable entity. In additional returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object <see cref="{TProjected}"/>
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        TProjected? GetSingle<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking and <see cref="Expression{Func}"/> where expression. This method performs get entity with apply filter async version. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        Task<TEntity?> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> and <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression. This method performs get entity with apply filter and includable entities async version. In additional this method returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        Task<TEntity?> GetSingleAsync<TEntity>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="Expression{Func}"/> project the expression and <see cref="CancellationToken"/> cancellation token. This method performs get projected object with apply filter async version. In additional returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        Task<TProjected?> GetSingleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="Expression{Func}"/> where expression, <see cref="Expression{Func}"/> project expression, <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression and <see cref="CancellationToken"/> cancellation token. This method performs get projected object with apply filter and includable entity async version. In additional returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object <see cref="{TProjected}"/>
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        Task<TProjected?> GetSingleAsync<TEntity, TProjected>(bool asNoTracking, Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, TProjected>> projectExpression, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking and <see cref="object"/> id. This method provides get entity by id. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity? GetById<TEntity>(bool asNoTracking, object id) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="object"/> id and <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression. This method performs get entity by id with includable entities. In additional this method returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by the EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        TEntity? GetById<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asnoTracking, <see cref="object"/> id and <see cref="Expression{Func}"/> project expression. This method performs get projected object by id with includable entities. In additional this method returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object <see cref="{TProjected}"/>
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by the EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        TProjected? GetById<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asnoTracking, <see cref="object"/> id, <see cref="IIncludableQueryable{TEntity, TProperty}"/> and <see cref="Expression{Func}"/> project expression. This method performs get projected object by id with includable entities. In additional this method returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object <see cref="{TProjected}"/>
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by the EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        TProjected? GetById<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="object"/> id. This method provides get entity by id async version. In additional returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        Task<TEntity?> GetByIdAsync<TEntity>(bool asNoTracking, object id, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asNoTracking, <see cref="object"/> id, <see cref="IIncludableQueryable{TEntity, TProperty}"/> include expression and <see cref="CancellationToken"/> cancellation token. This method performs get entity by id with includable entities. In additional this method returns <see cref="{TEntity}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by the EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TEntity}"/>
        /// </returns>
        Task<TEntity?> GetByIdAsync<TEntity>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asnoTracking, <see cref="object"/> id and <see cref="Expression{Func}"/> project expression. This method performs get projected object by id with includable entities async version. In additional this method returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object <see cref="{TProjected}"/>
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by the EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        Task<TProjected?> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="bool"/> asnoTracking, <see cref="object"/> id, <see cref="IIncludableQueryable{TEntity, TProperty}"/> and <see cref="Expression{Func}"/> project expression. This method performs get projected object by id with includable entities async version. In additional this method returns <see cref="{TProjected}"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <typeparam name="TProjected">
        /// Type of projected object <see cref="{TProjected}"/>
        /// </typeparam>
        /// <param name="asNoTracking">
        /// Do you want the entity to be tracked by the EF Core? Default value : false <see cref="bool"/>
        /// </param>
        /// <param name="id">
        /// PK of entity
        /// </param>
        /// <param name="includeExpression">
        /// Include expression <see cref="IIncludableQueryable{TEntity, TProperty}"/>
        /// </param>
        /// <param name="projectExpression">
        /// Project expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="{TProjected}"/>
        /// </returns>
        Task<TProjected?> GetByIdAsync<TEntity, TProjected>(bool asNoTracking, object id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeExpression, Expression<Func<TEntity, TProjected>> projectExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> any expression. This method perform exist operation for condition. In additional returns <see cref="bool"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="anyExpression">
        /// Any expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="bool"/>
        /// </returns>
        bool Any<TEntity>(Expression<Func<TEntity, bool>> anyExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> any expression and <see cref="CancellationToken"/> cancellation token. This method perform exist operation for condition. In additional returns <see cref="bool"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="anyExpression">
        /// Any expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="bool"/>
        /// </returns>
        Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> anyExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method performs get count information of entity. In additional returns <see cref="int"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <returns>
        /// Returns <see cref="int"/>
        /// </returns>
        int Count<TEntity>() where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/>. This method performs get count information of entity with filter. In additional returns <see cref="int"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <returns>
        /// Returns <see cref="int"/>
        /// </returns>
        int Count<TEntity>(Expression<Func<TEntity, bool>> whereExpression) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="CancellationToken"/> cancellation token. This method performs get count information of entity async version. In additional returns <see cref="int"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <returns>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// Returns <see cref="int"/>
        /// </returns>
        Task<int> CountAsync<TEntity>(CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// This method takes <see cref="Expression{Func}"/> and <see cref="CancellationToken"/> cancellation token. This method performs get count information of entity with filter async version. In additional returns <see cref="int"/>
        /// </summary>
        /// <typeparam name="TEntity">
        /// Type of entity
        /// </typeparam>
        /// <param name="whereExpression">
        /// Where expression <see cref="Expression{Func}"/>
        /// </param>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Returns <see cref="int"/>
        /// </returns>
        Task<int> CountAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression, CancellationToken cancellationToken = default) where TEntity : class;

        /* DATA COMMIT METHODS */

        /// <summary>
        /// This method provides save changes for changes in current transaction
        /// </summary>
        int Complete();

        /// <summary>
        /// This method takes <see cref="CancellationToken"/> cancellation token in additional this method provides save changes for changes in current transactions
        /// </summary>
        /// <param name="cancellationToken"> A <see cref="CancellationToken"/> to observe while waiting for the task to complete.</param>
        /// <returns>
        /// Task. <see cref="Task"/>
        /// </returns>
        Task<int> CompleteAsync(CancellationToken cancellationToken = default);

        /* MISC METHODS */

        /// <summary>
        /// Asynchronously executes a MySQL stored procedure with the specified name and parameters, returning the results as an enumerable of the specified type.
        /// </summary>
        /// <typeparam name="TResult">The type of the result objects returned by the stored procedure.</typeparam>
        /// <param name="storedProcedureName">The name of the MySQL stored procedure to execute.</param>
        /// <param name="parameters">A list of tuples containing parameter names and their string values to pass to the stored procedure.</param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>A task that represents the asynchronous operation, containing an enumerable of <see cref="TResult"/> results.</returns>
        Task<IEnumerable<TResult>> ExecuteStoredProcedureAsync<TResult>(string storedProcedureName, List<(string Name, string Value)> parameters, CancellationToken cancellationToken = default);

        /// <summary>
        /// Asynchronously performs a bulk update on the specified entity type in the database, based on a given predicate and property update expression.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to update.</typeparam>
        /// <param name="whereExpression">An expression to filter the entities to be updated.</param>
        /// <param name="setProperty">
        /// An expression that defines the properties to update and their new values,
        /// using the <see cref="SetPropertyCalls{TEntity}"/> API.
        /// </param>
        /// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
        /// <returns>
        /// A task representing the asynchronous operation, containing the number of affected rows.
        /// </returns>
        Task<int> ExecuteUpdateAsync<TEntity>(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setProperty, CancellationToken cancellationToken = default) where TEntity : class;
    }
}