using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROS.Common.Extensions;
using ROS.Contracts.ApiRoutes;
using ROS.Services.Product.Queries.GetListProduct;
using ROS.Services.Product.Queries.GetProductByIds;
using System.Threading.Tasks;

namespace ROS.Api.Controllers
{
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[AllowAnonymous]
		[HttpGet(ApiRoutes.Products.GET_LIST)]
		[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, VaryByQueryKeys = new[] { "*" })]
		public async Task<IActionResult> GetListAsync([FromQuery] GetListProductRequest request)
		{
			if (request.productIds.IsNotEmpty())
			{
				var data = new GetProductByIdsRequest(request.productIds);
				return await _mediator.Send(data);
			}

			return await _mediator.Send(request);
		}

		[AllowAnonymous]
		[HttpGet(ApiRoutes.Products.GET)]
		public async Task<IActionResult> GetAsync([FromQuery] GetListProductRequest request)
		{
			if (request.productIds.IsNotEmpty())
			{
				var data = new GetProductByIdsRequest(request.productIds);
				return await _mediator.Send(data);
			}

			return await _mediator.Send(request);
		}
	}
}
