using MediatR;
using ROS.Common.ApiResponse;
using ROS.Common.Enums;
using System;

namespace ROS.Services.Order.Queries.GetPagingListOrder
{
	public class GetPagingListOrderRequest : IRequest<ApiResult>
	{
		public int Page { get; set; } = 1;
		public int Size { get; set; } = 10;
		public OrderStatus? Status { get; set; }
		public DateTime? StartOrderTime { get; set; }
		public DateTime? EndOrderTime { get; set; }
		public SortType SortType { get; set; } = SortType.DESC;
		public OrderSortBy SortBy { get; set; } = OrderSortBy.ORDER_DATE;
	}

	public enum OrderSortBy
	{
		ORDER_DATE,
		TOTAL_PRICE,
	}
}
