using IdentityServer4.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROS.Common.Extensions;
using ROS.Contracts.ApiRoutes;
using ROS.Services.Order.Commands.CreateOrder;
using ROS.Services.Order.Commands.UpdateOrderStatus;
using ROS.Services.Order.Queries.GetOrderById;
using ROS.Services.Order.Queries.GetPagingListOrder;
using System.Threading.Tasks;

namespace ROS.Api.Controllers
{
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IMediator _mediator;
		public OrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet(ApiRoutes.Orders.GET_LIST)]
		public async Task<IActionResult> GetPagingListAsync([FromQuery] GetPagingListOrderRequest request)
		{
			return await _mediator.Send(request);
		}

		[HttpGet(ApiRoutes.Orders.GET)]
		public async Task<IActionResult> GetAsync([FromQuery] GetOrderByIdRequest request)
		{
			return await _mediator.Send(request);
		}

		[AllowAnonymous]
		[HttpPost(ApiRoutes.Orders.CREATE)]
		public async Task<IActionResult> CreateAsync(CreateOrderRequest request)
		{
			var userId = User?.GetSubjectId();
			request.UserId = userId.IsNotEmpty() ? int.Parse(userId) : null;

			return await _mediator.Send(request);
		}

		[HttpPut(ApiRoutes.Orders.UPDATE_STATUS)]
		public async Task<IActionResult> UpdateAsync(UpdateOrderStatusRequest request)
		{
			return await _mediator.Send(request);
		}
	}
}
