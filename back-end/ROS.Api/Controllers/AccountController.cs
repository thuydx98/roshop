using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ROS.Contracts.ApiRoutes;
using ROS.Services.Account.Commands.VerifyAccount;
using System.Threading.Tasks;

namespace ROS.Api.Controllers
{
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IMediator _mediator;
		public AccountController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[AllowAnonymous]
		[HttpPost(ApiRoutes.Account.VERIFY)]
		public async Task<IActionResult> VerifyAccountAsync(VerifyAccountRequest request)
		{
			return await _mediator.Send(request);
		}
	}
}
