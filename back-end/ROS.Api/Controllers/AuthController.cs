using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROS.Contracts.ApiRoutes;
using ROS.Services.Account.Commands.CreateAccount;
using System.Threading.Tasks;

namespace ROS.Api.Controllers
{
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IMediator _mediator;
		public AuthController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[AllowAnonymous]
		[HttpPost(ApiRoutes.Auth.REGISTER)]
		public async Task<IActionResult> RegisterAsync(CreateAccountRequest request)
		{
			return await _mediator.Send(request);
		}
	}
}
