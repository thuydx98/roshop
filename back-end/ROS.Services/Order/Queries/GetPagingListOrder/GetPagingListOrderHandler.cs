using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Common.Enums;
using ROS.Common.Extensions;
using ROS.Contracts.EntityFramework;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Order.ViewModels;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Order.Queries.GetPagingListOrder
{
	public class GetPagingListOrderHandler : IRequestHandler<GetPagingListOrderRequest, ApiResult>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ILogger _logger;

		public GetPagingListOrderHandler(IUnitOfWork<ReadDbContext> unitOfWork, ILogger<GetPagingListOrderHandler> logger)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(GetPagingListOrderRequest request, CancellationToken cancellationToken)
		{
            try
            {
				var predicate = BuildFilterQuery(request);
				var orderBy = BuildOrderQuery(request);
				var order = await _unitOfWork.GetRepository<OrderEntity>().GetPagingListAsync(
                    selector: n => new OrderViewModel(n),
                    predicate: predicate,
                    orderBy: orderBy,
					include: n => n.Include(i => i.Details),
                    page: request.Page,
                    size: request.Size,
                    cancellationToken: cancellationToken);

                if (order == null)
                {
                    return ApiResult.Failed(HttpCode.Notfound);
                }

                return ApiResult.Succeeded(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return ApiResult.Failed(HttpCode.InternalServerError);
            }
		}

		private static Func<IQueryable<OrderEntity>, IOrderedQueryable<OrderEntity>> BuildOrderQuery(GetPagingListOrderRequest request)
		{
			Expression<Func<OrderEntity, object>> expression = request.SortBy switch
			{
				OrderSortBy.ORDER_DATE => p => p.OrderedTime,
				OrderSortBy.TOTAL_PRICE => p => p.Total,
				_ => p => p.OrderedTime,
			};

			return request.SortType == SortType.ASC
				? o => o.OrderBy(expression)
				: o => o.OrderByDescending(expression);
		}

		private static Expression<Func<OrderEntity, bool>> BuildFilterQuery(GetPagingListOrderRequest request)
		{
			Expression<Func<OrderEntity, bool>> filterQuery = p => true;
			if (request.Status.HasValue)
			{
				filterQuery = filterQuery.AndAlso(o => o.Status == request.Status);
			}

			if (request.StartOrderTime.HasValue)
			{
				filterQuery = filterQuery.AndAlso(p => p.OrderedTime >= request.StartOrderTime);
			}

			if (request.EndOrderTime.HasValue)
			{
				filterQuery = filterQuery.AndAlso(p => p.OrderedTime <= request.EndOrderTime);
			}

			return filterQuery;
		}
	}
}
