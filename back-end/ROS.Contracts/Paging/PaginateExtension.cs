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
			int page,
			int size,
			int firstPage = 1,
			CancellationToken cancellationToken = default)
		{
			if (firstPage > page)
			{
				throw new ArgumentException($"page ({page}) must greater or equal than firstPage ({firstPage})");
			}

			var total = await queryable.CountAsync(cancellationToken);
			var items = await queryable
				.Skip((page - firstPage) * size)
				.Take(size)
				.ToListAsync(cancellationToken);

			return new Paginate<T>
			{
				Page = page,
				Size = size,
				Total = total,
				Items = items,
			};
		}
	}
}
