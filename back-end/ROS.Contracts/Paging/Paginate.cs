using System;
using System.Collections.Generic;
using System.Linq;

namespace ROS.Contracts.Paging
{
	public class Paginate<TResult> : IPaginate<TResult>
	{
		public int Size { get; set; }
		public int Page { get; set; }
		public int Total { get; set; }
		public IList<TResult> Items { get; set; }

		public Paginate(IEnumerable<TResult> source, int page, int size, int firstPage)
		{
			var enumerable = source as TResult[] ?? source.ToArray();
			if (firstPage > page)
			{
				throw new ArgumentException($"page ({page}) must greater or equal than firstPage ({firstPage})");
			}

			if (source is IQueryable<TResult> queryable)
			{
				Page = page;
				Size = size;
				Total = queryable.Count();
				Items = queryable.Skip((page - firstPage) * size).Take(size).ToList();
			}
			else
			{
				Page = page;
				Size = size;
				Total = enumerable.Length;
				Items = enumerable.Skip((page - firstPage) * size).Take(size).ToList();
			}
		}

		public Paginate()
		{
			Items = Array.Empty<TResult>();
		}
	}

	public class Paginate<TSource, TResult> : IPaginate<TResult>
	{
		public int Size { get; set; }
		public int Page { get; set; }
		public int Total { get; set; }
		public IList<TResult> Items { get; set; }

		public Paginate(
			IEnumerable<TSource> source,
			Func<IEnumerable<TSource>, IEnumerable<TResult>> converter,
			int page,
			int size,
			int firstPage)
		{
			var enumerable = source as TSource[] ?? source.ToArray();
			if (firstPage > page)
			{
				throw new ArgumentException($"page ({page}) must greater or equal than firstPage ({firstPage})");
			}

			if (source is IQueryable<TSource> queryable)
			{
				var items = queryable.Skip((page - firstPage) * size).Take(size).ToArray();

				Page = page;
				Size = size;
				Total = queryable.Count();
				Items = new List<TResult>(converter(items));
			}
			else
			{
				var items = enumerable.Skip((page - firstPage) * size).Take(size).ToArray();

				Page = page;
				Size = size;
				Total = enumerable.Length;
				Items = new List<TResult>(converter(items));
			}
		}

		public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
		{
			Page = source.Page;
			Size = source.Size;
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
