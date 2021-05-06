using MediatR;
using ROS.Common.ApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ROS.Services.Product.Queries.GetProductByIds
{
	public class GetProductByIdsRequest : IRequest<ApiResult>
	{
		public GetProductByIdsRequest(string ids)
		{
			Ids = ids.Split(',').Select(Int32.Parse).ToList();
		}

		public List<int> Ids { get; set; }
	}
}
