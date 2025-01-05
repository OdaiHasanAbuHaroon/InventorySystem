using InventorySystem.Shared.DTOs.BaseDTOs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace InventorySystem.Data.Repositories.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        DbContext Context { get; }
        void ClearChangeTracker();
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? condition, CancellationToken cancellationToken = default);
        Task<ICollection<TEntity>> GetAll();
        IQueryable<TEntity> GetQueryable();
        Task<object[]> InsertAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default);
        Task InsertAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(IEnumerable<Expression<Func<TEntity, bool>>> conditions, CancellationToken cancellationToken = default);
        Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default);
        Task<int> ExecuteUpdateAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default);
        Task UpdateAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, bool saveChanges = true, CancellationToken cancellationToken = default);
        Task DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default);
        Task DeleteAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default);
        Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> IGetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int? page = null, int? pageSize = null, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> IGetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int? page = null, int? pageSize = null, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IQueryable<TEntity>> FilterByAsync<TFilterable>(TFilterable filter, Expression<Func<TEntity, bool>>? filterExt = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, params Expression<Func<TEntity, object>>[] includeProperties) where TFilterable : FilterModel;
        Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
        Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters);
        Task<List<T>> ExecuteStoredProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class;
        Task<DataTable> ExecuteStoredProcedure(string connectionString, string storedProcedureName, SqlParameter[]? parameters = null);
        List<T> ConvertDataTableToList<T>(DataTable dt) where T : new();
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, CancellationToken cancellationToken = default);
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<DbParameter> parameters, CancellationToken cancellationToken = default);
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default);
        Task<List<T>> GetFromRawSqlAsync<T>(string sql, object parameter, CancellationToken cancellationToken = default);
    }
}