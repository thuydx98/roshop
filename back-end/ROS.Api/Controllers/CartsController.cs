using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ROS.Contracts.ApiRoutes;
using ROS.Services.Cart.Commands.UpdateCart;
using ROS.Services.Cart.Models;
using ROS.Services.Cart.Queries.GetCart;
using System.Threading.Tasks;

namespace ROS.Api.Controllers
{
	[ApiController]
	public class CartsController : ControllerBase
	{
		private readonly IMediator _mediator;
		public CartsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet(ApiRoutes.Carts.GET)]
		public async Task<IActionResult> GetCartAsync()
		{
			var request = new GetCartRequest(User.GetSubjectId());
			return await _mediator.Send(request);
		}

		[HttpPost(ApiRoutes.Carts.SYNC)]
		public async Task<IActionResult> SyncCartAsync(CartItemModel[] items)
		{
			var userId = User.GetSubjectId();
			var request = new UpdateCartRequest(userId, items);

			return await _mediator.Send(request);
		}

		[HttpPut(ApiRoutes.Carts.UPDATE)]
		public async Task<IActionResult> UpdateCartAsync(CartItemModel item)
		{
			var userId = User.GetSubjectId();
			var items = new CartItemModel[] { item };
			var request = new UpdateCartRequest(userId, items);

			return await _mediator.Send(request);
		}
	}
}
