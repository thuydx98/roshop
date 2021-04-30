namespace ROS.Common.ApiResponse
{
	public class ApiJsonResult<TResult>
	{
		public bool Success { get; set; }
		public TResult Result { get; set; }
		public int? ErrorCode { get; set; }
		public string ErrorMessage { get; set; }

		public ApiJsonResult(TResult result)
		{
			Success = true;
			Result = result;
		}

		public ApiJsonResult(bool success)
		{
			Success = success;
		}

		public ApiJsonResult(int errorCode, string errorMessage)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
		}

		public ApiJsonResult(int errorCode, string errorMessage, TResult result)
		{
			ErrorCode = errorCode;
			ErrorMessage = errorMessage;
			Result = result;
		}
	}
}
