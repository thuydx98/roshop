using MediatR;
using ROS.Common.ApiResponse;
using System.Collections.Generic;
using System.Linq;

namespace ROS.Services.Product.Queries.GetProductByIds
{
	public class GetProductByIdsRequest : IRequest<ApiResult>
	{
		public GetProductByIdsRequest(string ids)
		{
			Ids = ids.Split(',').OfType<string>().ToList();
		}

		public List<string> Ids { get; set; }
	}
}
