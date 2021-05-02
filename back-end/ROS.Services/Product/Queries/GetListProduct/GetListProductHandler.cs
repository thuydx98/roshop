using MediatR;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Common.Enums;
using ROS.Common.Extensions;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Product.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Product.Queries.GetListProduct
{
	public class GetListProductHandler : IRequestHandler<GetListProductRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public GetListProductHandler(IUnitOfWork<ReadDbContext> unitOfWork, ILogger<GetListProductHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(GetListProductRequest request, CancellationToken cancellationToken)
		{
			try
			{
				var selector = BuildSelectQuery();
				var predicate = BuildFilterQuery(request);
				var orderBy = BuildOrderQuery(request);
				var products = await _unitOfWork.GetRepository<ProductEntity>().GetPagingListAsync(
					selector: selector,
					predicate: predicate,
					orderBy: orderBy,
					page: request.Page,
					size: request.Size,
					cancellationToken: cancellationToken);

				return ApiResult.Succeeded(products);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}

		private Expression<Func<ProductEntity, ProductViewModel>> BuildSelectQuery() => p => new ProductViewModel()
		{
			Id = p.Id,
			Code = p.Code,
			Name = p.Name,
			Price = p.Price,
			Rate = p.Rate,
			AvatarUrl = p.AvatarUrl,
		};

		private Func<IQueryable<ProductEntity>, IOrderedQueryable<ProductEntity>> BuildOrderQuery(GetListProductRequest request)
		{
			var monthBefore = DateTime.UtcNow.AddMonths(-1);
			Expression<Func<ProductEntity, object>> expression = request.SortBy switch
			{
				SortBy.PRICE => p => p.Price,
				SortBy.LATEST => p => p.CreatedAt,
				SortBy.SALES => p => p.OrderDetails.Sum(d => d.Quantity),
				SortBy.TRENDING => p => p.OrderDetails.Where(o => o.CreatedAt >= monthBefore).Sum(d => d.Quantity),
				_ => p => p.OrderDetails.Sum(d => d.Quantity),
			};

			return request.SortType == SortType.ASC
				? o => o.OrderBy(expression)
				: o => o.OrderByDescending(expression);
		}

		private Expression<Func<ProductEntity, bool>> BuildFilterQuery(GetListProductRequest request)
		{
			Expression<Func<ProductEntity, bool>> filterQuery = p => true;
			if (request.Search.IsNotEmpty())
			{
				request.Search = request.Search.Trim().ToLower();
				filterQuery = filterQuery.AndAlso(p => p.Name.ToLower().Contains(request.Search));
			}

			if (request.CategoryId.HasValue)
			{
				filterQuery = filterQuery.AndAlso(p => p.ProductCategories.Where(c => c.CategoryId == request.CategoryId).Any());
			}

			if (request.BrandId.HasValue)
			{
				filterQuery = filterQuery.AndAlso(p => p.BrandId == request.BrandId);
			}

			if (request.MinPrice.HasValue)
			{
				filterQuery = filterQuery.AndAlso(p => p.Price >= request.MinPrice);
			}

			if (request.MaxPrice.HasValue)
			{
				filterQuery = filterQuery.AndAlso(p => p.Price <= request.MaxPrice);
			}

			return filterQuery;
		}
	}
}
