using MediatR;
using ROS.Common.ApiResponse;

namespace ROS.Services.Cart.Queries.GetCart
{
	public class GetCartRequest : IRequest<ApiResult>
	{
		public GetCartRequest(string userId)
		{
			UserId = int.Parse(userId);
		}

		public int UserId { get; set; }
	}
}
