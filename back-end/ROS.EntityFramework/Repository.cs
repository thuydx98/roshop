using ROS.Contracts.EntityFramework;
using ROS.Contracts.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.EntityFramework
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly DbContext _dbContext;
		protected readonly DbSet<T> _dbSet;

		public Repository(DbContext context)
		{
			_dbContext = context;
			_dbSet = context.Set<T>();
		}

		#region Dispose
		public void Dispose()
		{
			_dbContext?.Dispose();
		}
		#endregion

		#region Get Async
		public virtual async Task<T> SingleOrDefaultAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			bool ignoreQueryFilters = false,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet;

			if (asNoTracking) query = query.AsNoTracking();

			if (include != null) query = include(query);

			if (predicate != null) query = query.Where(predicate);

			if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

			if (orderBy != null) return await orderBy(query).FirstOrDefaultAsync();

			return await query.FirstOrDefaultAsync(cancellationToken);
		}

		public virtual async Task<TResult> SingleOrDefaultAsync<TResult>(
			Expression<Func<T, TResult>> selector,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			bool ignoreQueryFilters = false,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet;

			if (asNoTracking) query = query.AsNoTracking();

			if (include != null) query = include(query);

			if (predicate != null) query = query.Where(predicate);

			if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

			if (orderBy != null) return await orderBy(query).Select(selector).FirstOrDefaultAsync();

			return await query.Select(selector).FirstOrDefaultAsync(cancellationToken);
		}

		public async Task<ICollection<T>> GetListAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet;

			if (asNoTracking) query = query.AsNoTracking();

			if (include != null) query = include(query);

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return await orderBy(query).ToListAsync(cancellationToken);

			return await query.ToListAsync(cancellationToken);
		}

		public async Task<ICollection<TResult>> GetListAsync<TResult>(
			Expression<Func<T, TResult>> selector,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			bool ignoreQueryFilters = false,
			CancellationToken cancellationToken = default) where TResult : class
		{
			IQueryable<T> query = _dbSet;

			if (asNoTracking) query = query.AsNoTracking();

			if (include != null) query = include(query);

			if (predicate != null) query = query.Where(predicate);

			if (ignoreQueryFilters) query = query.IgnoreQueryFilters();

			if (orderBy != null)
				return await orderBy(query).Select(selector).ToListAsync(cancellationToken);

			return await query.Select(selector).ToListAsync(cancellationToken);
		}

		public Task<IPaginate<T>> GetPagingListAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			int pageIndex = 1,
			int pageSize = 20,
			bool asNoTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet;
			if (asNoTracking) query = query.AsNoTracking();

			if (include != null) query = include(query);

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return orderBy(query).ToPaginateAsync(pageIndex, pageSize, 1, cancellationToken);

			return query.ToPaginateAsync(pageIndex, pageSize, 1, cancellationToken);
		}

		public Task<IPaginate<TResult>> GetPagingListAsync<TResult>(
			Expression<Func<T, TResult>> selector,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			int pageIndex = 1,
			int pageSize = 20,
			bool asNoTracking = true,
			CancellationToken cancellationToken = default)
		{
			IQueryable<T> query = _dbSet;

			if (asNoTracking) query = query.AsNoTracking();

			if (include != null) query = include(query);

			if (predicate != null) query = query.Where(predicate);

			if (orderBy != null)
				return orderBy(query).Select(selector).ToPaginateAsync(pageIndex, pageSize, 1, cancellationToken);

			return query.Select(selector).ToPaginateAsync(pageIndex, pageSize, 1, cancellationToken);
		}
		#endregion

		#region Insert
		public virtual T Insert(T entity)
		{
			return _dbSet.Add(entity).Entity;
		}

		public void Insert(params T[] entities)
		{
			_dbSet.AddRange(entities);
		}

		public void Insert(IEnumerable<T> entities)
		{
			_dbSet.AddRange(entities);
		}
		#endregion

		#region Insert Async
		public virtual ValueTask<EntityEntry<T>> InsertAsync(T entity, CancellationToken cancellationToken = default)
		{
			return _dbSet.AddAsync(entity, cancellationToken);
		}

		public virtual Task InsertAsync(params T[] entities)
		{
			return _dbSet.AddRangeAsync(entities);
		}

		public virtual Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
		{
			return _dbSet.AddRangeAsync(entities, cancellationToken);
		}
		#endregion

		#region Update
		public T Update(T entity)
		{
			return _dbSet.Update(entity).Entity;
		}

		public void Update(params T[] entities)
		{
			_dbSet.UpdateRange(entities);
		}

		public void Update(IEnumerable<T> entities)
		{
			_dbSet.UpdateRange(entities);
		}
		#endregion
		
		#region Delete
		public T Delete(T entity)
		{
			return _dbSet.Remove(entity).Entity;
		}

		public T Delete(params object[] keyValues)
		{
			var entities = _dbSet.Find(keyValues);
			if (entities == null)
			{
				throw new ArgumentNullException(nameof(entities));
			}
			
			return _dbSet.Remove(entities).Entity;
		}

		public void Delete(params T[] entities)
		{
			_dbSet.RemoveRange(entities);
		}

		public void Delete(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
		}
		#endregion
	}
}
