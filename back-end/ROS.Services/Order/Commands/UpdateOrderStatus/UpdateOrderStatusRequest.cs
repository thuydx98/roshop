using MediatR;
using ROS.Common.ApiResponse;
using ROS.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace ROS.Services.Order.Commands.UpdateOrderStatus
{
	public class UpdateOrderStatusRequest : IRequest<ApiResult>
	{
		[Required]
		public string OrderId { get; set; }
		[Required]
		public OrderStatus Status { get; set; }
	}
}
