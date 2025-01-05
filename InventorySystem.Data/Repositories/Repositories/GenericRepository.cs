using InventorySystem.Shared.DTOs;
using InventorySystem.Shared.DTOs.BaseDTOs;
using InventorySystem.Shared.Entities.BaseEntities;
using InventorySystem.Shared.Enumerations;
using InventorySystem.Shared.Interfaces;
using InventorySystem.Data.Repositories.IRepositories;
using LinqKit;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Collections.Concurrent;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Reflection;

namespace InventorySystem.Data.Repositories.Repositories
{
    public class GenericRepository<TEntity>(DbContext context) : IGenericRepository<TEntity> where TEntity : class, ISoftDeletable
    {

        public DbContext Context { get; private set; } = context;
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? condition, CancellationToken cancellationToken = default)
        {
            if (condition == null)
            {
                return await _dbSet.AnyAsync(cancellationToken);
            }
            bool isExists = await _dbSet.AnyAsync(condition, cancellationToken).ConfigureAwait(false);
            return isExists;
        }

        public async Task<ICollection<TEntity>> GetAll()
        {
            return await _dbSet.Where(x => x.IsDeleted == false).AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _dbSet;
        }
        public void ClearChangeTracker()
        {
            Context.ChangeTracker.Clear();
        }

        #region SqlCommand

        public Task<int> ExecuteSqlCommandAsync(string sql, CancellationToken cancellationToken = default)
        {
            return Context.Database.ExecuteSqlRawAsync(sql, cancellationToken);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlRawAsync(sql, parameters);
        }

        public Task<int> ExecuteSqlCommandAsync(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            return Context.Database.ExecuteSqlRawAsync(sql, parameters, cancellationToken);
        }
        public async Task<List<T>> ExecuteStoredProcedureAsync<T>(string procedureName, params DbParameter[] parameters) where T : class
        {
            if (string.IsNullOrWhiteSpace(procedureName))
            {
                throw new ArgumentNullException(nameof(procedureName));
            }

            var commandText = $"EXEC {procedureName}";

            return await Context.Set<T>().FromSqlRaw(commandText, parameters).ToListAsync();
        }

        #endregion

        #region FromRawSql

        public async Task<List<T>> GetFromRawSqlAsync<T>(string sql, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            IEnumerable<object> parameters = [];

            List<T> items = await Context.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public async Task<List<T>> GetFromRawSqlAsync<T>(string sql, object parameter, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<object> parameters = new() { parameter };
            List<T> items = await Context.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public async Task<List<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<DbParameter> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<T> items = await Context.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public async Task<List<T>> GetFromRawSqlAsync<T>(string sql, IEnumerable<object> parameters, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(sql))
            {
                throw new ArgumentNullException(nameof(sql));
            }

            List<T> items = await Context.GetFromQueryAsync<T>(sql, parameters, cancellationToken);
            return items;
        }

        public List<T> ConvertDataTableToList<T>(DataTable dt) where T : new()
        {
            // Cache property info to avoid repeated reflection calls
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(p => p.CanWrite && dt.Columns.Contains(p.Name))
                                      .ToDictionary(p => p.Name, p => p);

            List<T> list = [];

            foreach (DataRow row in dt.Rows)
            {
                T item = new();

                foreach (DataColumn column in dt.Columns)
                {
                    if (properties.TryGetValue(column.ColumnName, out PropertyInfo? property))
                    {
                        object? value = row[column] != DBNull.Value ? row[column] : null;

                        // Safe type conversion
                        if (value != null && property.PropertyType.IsAssignableFrom(value.GetType()))
                        {
                            property.SetValue(item, value);
                        }
                        else if (value != null)
                        {
                            // Convert to the target property type
                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                }

                list.Add(item);
            }

            return list;
        }

        public async Task<DataTable> ExecuteStoredProcedure(string connectionString, string storedProcedureName, SqlParameter[]? parameters = null)
        {
            await using SqlConnection connection = new(connectionString);
            await using SqlCommand command = new(storedProcedureName, connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();
            command.CommandTimeout = 600;

            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }

            await connection.OpenAsync();
            using SqlDataReader reader = await command.ExecuteReaderAsync();
            DataTable dataTable = new();
            dataTable.Load(reader);
            return dataTable;
        }

        #endregion

        #region Insert  

        public async Task<object[]> InsertAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            // Add entity and save changes
            EntityEntry<TEntity> entityEntry = await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);

            if (saveChanges)
            {
                await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

                var primaryKey = entityEntry.Metadata.FindPrimaryKey();
                if (primaryKey == null)
                {
                    return [];
                }

                var primaryKeyValues = new object[primaryKey.Properties.Count];
                for (int i = 0; i < primaryKey.Properties.Count; i++)
                {
                    var currentValue = entityEntry.Property(primaryKey.Properties[i].Name).CurrentValue;
                    primaryKeyValues[i] = currentValue ?? DBNull.Value;
                }

                return primaryKeyValues;
            }

            return [];
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            await _dbSet.AddRangeAsync(entities, cancellationToken).ConfigureAwait(false);

            if (saveChanges)
            {
                await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region GetCountAsync

        public async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            long count = await _dbSet.LongCountAsync(cancellationToken).ConfigureAwait(false);
            return count;
        }

        public async Task<long> GetCountAsync(Expression<Func<TEntity, bool>> condition, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            if (condition != null)
            {
                query = query.Where(condition);
            }

            return await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
        }

        public async Task<long> GetCountAsync(IEnumerable<Expression<Func<TEntity, bool>>> conditions, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> query = _dbSet;

            if (conditions != null)
            {
                foreach (Expression<Func<TEntity, bool>> expression in conditions)
                {
                    query = query.Where(expression);
                }
            }

            return await query.LongCountAsync(cancellationToken).ConfigureAwait(false);
        }

        #endregion

        #region Update

        public async Task<int> ExecuteUpdateAsync(Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default)
        {
            int count = await _dbSet.ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
            return count;
        }

        public async Task<int> ExecuteUpdateAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<SetPropertyCalls<TEntity>, SetPropertyCalls<TEntity>>> setPropertyCalls, CancellationToken cancellationToken = default)
        {
            int count = await _dbSet.Where(condition).ExecuteUpdateAsync(setPropertyCalls, cancellationToken);
            return count;
        }

        public async Task UpdateAsync(TEntity entity, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            _dbSet.Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            if (saveChanges)
            {
                await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities, bool saveChanges = false, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            // Attach each entity to the context and mark it as modified
            foreach (var entity in entities)
            {
                _dbSet.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
            }

            if (saveChanges)
            {
                await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        #endregion

        #region Delete

        public async Task<bool> DeleteAsync(long id, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            TEntity? entityToDelete = await _dbSet.FindAsync(id);
            if (entityToDelete == null)
            {
                // Optionally log or track the fact that the entity wasn't found
                return false; // Indicate that no entity was deleted
            }
            entityToDelete.IsDeleted = true;
            await UpdateAsync(entityToDelete, saveChanges, cancellationToken);
            return true;
        }

        public async Task DeleteAsync(TEntity entity, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entity);

            entity.IsDeleted = true;
            await UpdateAsync(entity, saveChanges, cancellationToken);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities, bool saveChanges = true, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(entities);

            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
            }

            await UpdateAsync(entities, saveChanges, cancellationToken);
        }

        #endregion

        #region Get

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply includes (navigation properties)
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Check for soft delete filter
            query = query.Where(x => !x.IsDeleted);

            // Apply the predicate
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Apply ordering if provided
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            // Return the first result or null if none is found
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int? page = null, int? pageSize = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Start with the base DbSet
            IQueryable<TEntity> query = _dbSet;

            // Apply soft delete filter (assumes entities have IsDeleted property)
            query = query.Where(x => !x.IsDeleted);

            // Apply predicate (filter)
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Apply eager loading (includes for complex navigation properties)
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            // Apply eager loading for basic navigation properties
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Apply ordering (orderBy)
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Apply pagination directly if both page and pageSize are provided
            if (page.HasValue && pageSize.HasValue)
            {
                int skip = (page.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            else if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public IQueryable<TEntity> IGetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            // Apply includes (navigation properties)
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Check for soft delete filter
            query = query.Where(x => !x.IsDeleted);

            // Apply the predicate
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Apply ordering if provided
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public IQueryable<TEntity> IGetListAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, int? page = null, int? pageSize = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            // Start with the base DbSet
            IQueryable<TEntity> query = _dbSet;

            // Apply soft delete filter (assumes entities have IsDeleted property)
            query = query.Where(x => !x.IsDeleted);

            // Apply predicate (filter)
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            // Apply eager loading (includes for complex navigation properties)
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            // Apply eager loading for basic navigation properties
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Apply ordering (orderBy)
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // Apply pagination directly if both page and pageSize are provided
            if (page.HasValue && pageSize.HasValue)
            {
                int skip = (page.Value - 1) * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }
            else if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return query;
        }

        #endregion

        public async Task<IQueryable<TEntity>> FilterByAsync<TFilterable>(TFilterable filter, Expression<Func<TEntity, bool>>? filterExt = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object?>>[]? includes = null, params Expression<Func<TEntity, object>>[] includeProperties) where TFilterable : FilterModel
        {
            IQueryable<TEntity> query = _dbSet;
            // Apply soft delete filter
            query = query.Where(x => !x.IsDeleted);

            // Apply external filter if provided
            if (filterExt != null)
            {
                query = query.Where(filterExt);
            }

            // Apply predicate built from the filter
            var predicate = BuildPredicate<TEntity, TFilterable>(filter);
            query = query.AsExpandableEFCore().Where(predicate);

            // Apply eager loading for complex navigation properties
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            // Apply eager loading for basic navigation properties
            if (includeProperties != null && includeProperties.Length > 0)
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);
                }
            }

            // Count total records (for pagination purposes)
            filter.TotalRecords = await query.CountAsync();

            // Apply sorting if available
            if (!string.IsNullOrEmpty(filter.SortParameter))
            {
                bool isAscending = filter.SortDirection ?? true;
                query = isAscending
                    ? query.OrderBy(x => EF.Property<object>(x, filter.SortParameter))
                    : query.OrderByDescending(x => EF.Property<object>(x, filter.SortParameter));
            }
            else
            {
                // Default to sorting by "Id" if no sort parameter is provided and entity is a subclass of BaseEntity
                if (typeof(TEntity).IsSubclassOf(typeof(BaseEntity)))
                {
                    filter.SortParameter = "Id";
                    bool isAscending = filter.SortDirection ?? true;
                    query = isAscending
                        ? query.OrderBy(x => EF.Property<object>(x, filter.SortParameter))
                        : query.OrderByDescending(x => EF.Property<object>(x, filter.SortParameter));
                }
            }

            // Apply pagination if requested
            if (filter.PageSize.HasValue && filter.CurrentPage.HasValue)
            {
                int skip = (filter.CurrentPage.Value - 1) * filter.PageSize.Value;
                query = query.Skip(skip).Take(filter.PageSize.Value);
            }
            return query;
        }

        #region BuildPredicate

        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _filterPropertyCache = new();

        private static Expression<Func<T, bool>> BuildPredicate<T, TFilterable>(TFilterable filter) where TFilterable : FilterModel
        {
            var predicate = PredicateBuilder.New<T>(true); // Initialize with a default 'true' predicate.

            // Retrieve cached filter properties or populate the cache
            var filterProperties = GetFilterableProperties<TFilterable>();

            foreach (var filterProperty in filterProperties)
            {
                var value = filterProperty.GetValue(filter);
                if (value == null) continue;

                // Get the FilterableAttribute directly
                var filterAttribute = (FilterableAttribute)Attribute.GetCustomAttribute(filterProperty, typeof(FilterableAttribute))!;
                var propertyChain = BuildPropertyChain(filterAttribute.PropertyChain, filterProperty.Name);

                // Apply the filter based on the property chain and criteria
                var lambda = ApplyFilter(propertyChain, value, typeof(T), filterAttribute.FilterCriteria) as Expression<Func<T, bool>>;
                if (lambda != null)
                {
                    predicate = predicate.And(lambda);
                }
            }

            return predicate;
        }

        // Helper to cache and retrieve filter properties with FilterableAttribute
        private static List<PropertyInfo> GetFilterableProperties<TFilterable>() where TFilterable : FilterModel
        {
            var type = typeof(TFilterable);
            if (_filterPropertyCache.TryGetValue(type, out var cachedProperties))
            {
                return cachedProperties;
            }

            var properties = type.GetProperties()
                .Where(prop => Attribute.IsDefined(prop, typeof(FilterableAttribute), false))
                .ToList();

            _filterPropertyCache[type] = properties;
            return properties;
        }

        // Helper method to build the property chain
        private static List<string> BuildPropertyChain(string? propertyChainString, string defaultProperty)
        {
            return string.IsNullOrEmpty(propertyChainString)
                ? [defaultProperty]
                : propertyChainString.Split('.').ToList();
        }

        private static Expression? ApplyFilter(List<string> propertyChain, object value, Type type, FilterCriteria filterCriteria)
        {
            try
            {
                ParameterExpression args = Expression.Parameter(type, type.Name.First() + propertyChain.Count.ToString());
                Expression? expression = args;

                for (int i = 0; i < propertyChain.Count; i++)
                {
                    string propertyName = propertyChain[i];
                    if (!TryGetCachedProperty(type, propertyName, out var property)) return null; // Property not found, return null.

                    if (property != null)
                    {
                        expression = Expression.Property(expression, property);
                        type = property.PropertyType;

                        // Apply filter criteria based on the value
                        if (value != null)
                        {
                            expression = ApplyCriteria(expression, value, type, filterCriteria);
                            if (expression == null) return null;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                return Expression.Lambda(expression, args);
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Helper to cache and retrieve properties using reflection
        private static readonly ConcurrentDictionary<(Type, string), PropertyInfo?> _propertyCache = new();

        private static bool TryGetCachedProperty(Type type, string propertyName, out PropertyInfo? property)
        {
            var key = (type, propertyName);
            if (_propertyCache.TryGetValue(key, out property)) return property != null;

            property = type.GetProperty(propertyName);
            _propertyCache[key] = property;
            return property != null;
        }

        // Apply the appropriate filter criteria based on the type and value
        private static Expression? ApplyCriteria(Expression expression, object value, Type type, FilterCriteria filterCriteria)
        {
            var valueType = value.GetType();

            // Handle string type criteria
            if (valueType == typeof(string))
            {
                return ApplyStringCriteria(expression, value, filterCriteria);
            }

            // Handle DateTime criteria
            if (valueType == typeof(DateTime))
            {
                return ApplyDateCriteria(expression, value, filterCriteria);
            }

            // Handle non-collection types
            if (!typeof(IEnumerable).IsAssignableFrom(type) || type == typeof(string))
            {
                var constant = Expression.Constant(value);
                return filterCriteria switch
                {
                    FilterCriteria.Equal => Expression.Equal(expression, constant),
                    FilterCriteria.NotEqual => Expression.NotEqual(expression, constant),
                    FilterCriteria.Start => Expression.GreaterThanOrEqual(expression, constant),
                    FilterCriteria.End => Expression.LessThanOrEqual(expression, constant),
                    _ => Expression.Equal(expression, constant)
                };
            }

            // Handle collections
            return ApplyCollectionCriteria(expression, value, type, valueType, filterCriteria);
        }

        // Apply filter criteria for string values
        private static Expression ApplyStringCriteria(Expression expression, object value, FilterCriteria filterCriteria)
        {
            // Dictionary to map FilterCriteria to string methods
            var methodMap = new Dictionary<FilterCriteria, string>
                {
                    { FilterCriteria.Start, "StartsWith" },
                    { FilterCriteria.End, "EndsWith" },
                    { FilterCriteria.Contains, "Contains" },
                    { FilterCriteria.Equal, "Equals" }
                };

            // Get the method name from the map, defaulting to "Equals" if not found
            string methodName = methodMap.ContainsKey(filterCriteria) ? methodMap[filterCriteria] : "Equals";

            // Retrieve the method info
            MethodInfo? stringMethod = typeof(string).GetMethod(methodName, [typeof(string)]) ?? throw new InvalidOperationException($"The method '{methodName}' could not be found on type 'string'.");

            // Return the expression call with the found method
            return Expression.Call(expression, stringMethod, Expression.Constant(value));
        }

        // Apply filter criteria for DateTime values
        private static Expression ApplyDateCriteria(Expression expression, object value, FilterCriteria filterCriteria)
        {
            var constant = Expression.Constant(value);
            return filterCriteria switch
            {
                FilterCriteria.Equal => Expression.Equal(expression, constant),
                FilterCriteria.NotEqual => Expression.NotEqual(expression, constant),
                FilterCriteria.Start => Expression.GreaterThanOrEqual(expression, constant),
                FilterCriteria.End => Expression.LessThanOrEqual(expression, constant),
                _ => Expression.Equal(expression, constant)
            };
        }

        // Apply filter criteria for collection types
        private static Expression? ApplyCollectionCriteria(Expression expression, object value, Type type, Type valueType, FilterCriteria filterCriteria)
        {
            if (typeof(IEnumerable).IsAssignableFrom(valueType) && valueType != typeof(string))
            {
                Type itemType = valueType.GetElementType() ?? valueType.GetGenericArguments()[0];
                if (itemType != null && (itemType.IsAssignableFrom(type) || type.IsAssignableFrom(itemType)))
                {
                    MethodInfo containsMethod = typeof(Enumerable).GetMethods()
                        .Single(m => m.Name == "Contains" && m.GetParameters().Length == 2)
                        .MakeGenericMethod(itemType);

                    Expression constant = Expression.Constant(value);

                    return Expression.Call(containsMethod, constant, expression);
                }
            }

            return null;
        }

        #endregion
    }
}
