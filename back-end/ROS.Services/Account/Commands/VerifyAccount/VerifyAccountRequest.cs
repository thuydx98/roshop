using MediatR;
using ROS.Common.ApiResponse;
using System.ComponentModel.DataAnnotations;

namespace ROS.Services.Account.Commands.VerifyAccount
{
	public class VerifyAccountRequest : IRequest<ApiResult>
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }

		[Required]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required]
		[StringLength( 6, MinimumLength = 6)]
		public string VerifyCode { get; set; }
	}
}
