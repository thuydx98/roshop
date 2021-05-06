using MediatR;
using ROS.Common.ApiResponse;
using System.ComponentModel.DataAnnotations;

namespace ROS.Services.Order.Queries.GetOrderById
{
	public class GetOrderByIdRequest : IRequest<ApiResult>
	{
		[Required]
		public string Id { get; set; }
	}
}
