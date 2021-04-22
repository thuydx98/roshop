using ROS.Contracts.Paging;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Contracts.EntityFramework
{
	public interface IRepository<T> : IDisposable where T : class
	{
		#region Get Async
		Task<T> SingleOrDefaultAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			bool ignoreQueryFilters = false,
			CancellationToken cancellationToken = default);

		Task<TResult> SingleOrDefaultAsync<TResult>(
			Expression<Func<T, TResult>> selector,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			bool ignoreQueryFilters = false,
			CancellationToken cancellationToken = default);

		Task<ICollection<T>> GetListAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			CancellationToken cancellationToken = default);

		Task<ICollection<TResult>> GetListAsync<TResult>(
			Expression<Func<T, TResult>> selector,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			bool asNoTracking = true,
			bool ignoreQueryFilters = false,
			CancellationToken cancellationToken = default) where TResult : class;

		Task<IPaginate<T>> GetPagingListAsync(
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			int pageIndex = 1,
			int pageSize = 20,
			bool asNoTracking = true,
			CancellationToken cancellationToken = default);

		Task<IPaginate<TResult>> GetPagingListAsync<TResult>(
			Expression<Func<T, TResult>> selector,
			Expression<Func<T, bool>> predicate = null,
			Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
			Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
			int pageIndex = 1,
			int pageSize = 20,
			bool asNoTracking = true,
			CancellationToken cancellationToken = default);
		#endregion

		#region Insert
		T Insert(T entity);
		void Insert(params T[] entities);
		void Insert(IEnumerable<T> entities);
		#endregion

		#region Insert Async
		ValueTask<EntityEntry<T>> InsertAsync(T entity, CancellationToken cancellationToken = default);
		Task InsertAsync(params T[] entities);
		Task InsertAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
		#endregion

		#region Update
		T Update(T entity);
		void Update(params T[] entities);
		void Update(IEnumerable<T> entities);
		#endregion

		#region Delete
		T Delete(T entity);
		T Delete(params object[] keyValues);
		void Delete(params T[] entities);
		void Delete(IEnumerable<T> entities);
		#endregion
	}
}