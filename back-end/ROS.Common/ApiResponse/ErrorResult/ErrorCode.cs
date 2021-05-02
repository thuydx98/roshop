using System.ComponentModel;

namespace ROS.Common.ApiResponse.ErrorResult
{
	public enum ErrorCode
	{
		[Description("User has already existed")]
		EXISTED_USER = 1000,
		[Description("Invalid verify code")]
		INVALID_VERIFY_CODE = 1001,
		[Description("The verify code was expired")]
		EXPIRED_CODE = 1002,
		[Description("The account was verified")]
		VERIFIED_ACCOUNT = 1003,
	}
}
