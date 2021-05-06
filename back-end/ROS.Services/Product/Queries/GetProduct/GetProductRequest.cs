using MediatR;
using ROS.Common.ApiResponse;

namespace ROS.Services.Product.Queries.GetProduct
{
	public class GetProductRequest : IRequest<ApiResult>
	{
		public int Id { get; set; }
	}
}
