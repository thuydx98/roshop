using ROS.Common.ApiResponse;
using MediatR;
using ROS.Common.Enums;

namespace ROS.Services.Product.Queries.GetListProduct
{
	public class GetListProductRequest : IRequest<ApiResult>
	{
		public int Page { get; set; } = 1;
		public int Size { get; set; } = 20;
		public string Search { get; set; }
		public SortType SortType { get; set; } = SortType.DESC;
		public SortBy SortBy { get; set; } = SortBy.SALES;
		public int? CategoryId { get; set; }
		public int? BrandId { get; set; }
		public double? MinPrice { get; set; }
		public double? MaxPrice { get; set; }
	}

	public enum SortBy
	{
		PRICE,
		SALES,
		TRENDING,
		LATEST
	}
}
