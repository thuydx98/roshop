using MediatR;
using ROS.Common.ApiResponse;

namespace ROS.Services.Account.Commands.CreateAccount
{
	public class CreateAccountRequest : IRequest<ApiResult>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
