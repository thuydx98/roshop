using MediatR;
using ROS.Common.ApiResponse;
using System.ComponentModel.DataAnnotations;

namespace ROS.Services.Account.Commands.CreateAccount
{
	public class CreateAccountRequest : IRequest<ApiResult>
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }
	}
}
