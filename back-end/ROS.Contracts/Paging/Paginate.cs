using System;
using System.Collections.Generic;
using System.Linq;

namespace ROS.Contracts.Paging
{
	public class Paginate<TResult> : IPaginate<TResult>
	{
		public int PageSize { get; set; }
		public int PageIndex { get; set; }
		public int Total { get; set; }
		public IList<TResult> Items { get; set; }

		public Paginate(IEnumerable<TResult> source, int pageIndex, int pageSize, int firstPage)
		{
			var enumerable = source as TResult[] ?? source.ToArray();
			if (firstPage > pageIndex)
			{
				throw new ArgumentException($"pageIndex ({pageIndex}) must greater or equal than firstPage ({firstPage})");
			}

			if (source is IQueryable<TResult> queryable)
			{
				PageIndex = pageIndex;
				PageSize = pageSize;
				Total = queryable.Count();
				Items = queryable.Skip((pageIndex - firstPage) * pageSize).Take(pageSize).ToList();
			}
			else
			{
				PageIndex = pageIndex;
				PageSize = pageSize;
				Total = enumerable.Length;
				Items = enumerable.Skip((pageIndex - firstPage) * pageSize).Take(pageSize).ToList();
			}
		}

		public Paginate()
		{
			Items = Array.Empty<TResult>();
		}
	}

	public class Paginate<TSource, TResult> : IPaginate<TResult>
	{
		public int PageSize { get; set; }
		public int PageIndex { get; set; }
		public int Total { get; set; }
		public IList<TResult> Items { get; set; }

		public Paginate(
			IEnumerable<TSource> source,
			Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
			int pageIndex,
			int pageSize,
			int firstPage)
		{
			var enumerable = source as TSource[] ?? source.ToArray();
			if (firstPage > pageIndex)
			{
				throw new ArgumentException($"pageIndex ({pageIndex}) must greater or equal than firstPage ({firstPage})");
			}

			if (source is IQueryable<TSource> queryable)
			{
				var items = queryable.Skip((pageIndex - firstPage) * pageSize).Take(pageSize).ToArray();

				PageIndex = pageIndex;
				PageSize = pageSize;
				Total = queryable.Count();
				Items = new List<TResult>(converter(items));
			}
			else
			{
				var items = enumerable.Skip((pageIndex - firstPage) * pageSize).Take(pageSize).ToArray();

				PageIndex = pageIndex;
				PageSize = pageSize;
				Total = enumerable.Length;
				Items = new List<TResult>(converter(items));
			}
		}

		public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
		{
			PageIndex = source.PageIndex;
			PageSize = source.PageSize;
			Total = source.Total;
			Items = new List<TResult>(converter(source.Items));
		}
	}

	public static class Paginate
	{
		public static IPaginate<T> Empty<T>()
		{
			return new Paginate<T>();
		}

		public static IPaginate<TResult> From<TResult, TSource>(
			IPaginate<TSource> source,
			Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
		{
			return new Paginate<TSource, TResult>(source, converter);
		}
	}
}
