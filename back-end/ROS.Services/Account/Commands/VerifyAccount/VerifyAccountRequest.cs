using MediatR;
using ROS.Common.ApiResponse;

namespace ROS.Services.Account.Commands.VerifyAccount
{
	public class VerifyAccountRequest : IRequest<ApiResult>
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string VerifyCode { get; set; }
	}
}
