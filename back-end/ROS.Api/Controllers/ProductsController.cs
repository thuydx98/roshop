using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROS.Contracts.ApiRoutes;
using ROS.Services.Product.Queries.GetListProduct;
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
		public async Task<IActionResult> GetListAsync([FromQuery] GetListProductQuery request)
		{
			return await _mediator.Send(request);
		}
	}
}
