using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Contracts.Paging
{
	public static class PaginateExtension
	{
		public static async Task<IPaginate<T>> ToPaginateAsync<T>(
			this IQueryable<T> queryable,
			int pageIndex,
			int pageSize,
			int firstPage = 1,
			CancellationToken cancellationToken = default)
		{
			if (firstPage > pageIndex)
			{
				throw new ArgumentException($"pageIndex ({pageIndex}) must greater or equal than firstPage ({firstPage})");
			}

			var total = await queryable.CountAsync(cancellationToken);
			var items = await queryable
				.Skip((pageIndex - firstPage) * pageSize)
				.Take(pageSize)
				.ToListAsync(cancellationToken);

			return new Paginate<T>
			{
				PageIndex = pageIndex,
				PageSize = pageSize,
				Total = total,
				Items = items,
			};
		}
	}
}
