namespace ROS.Services.Providers.Models
{
	public class AppleJwksModel
	{
		public string Kty { get; set; }
		public string Kid { get; set; }
		public string Use { get; set; }
		public string Alg { get; set; }
		public string N { get; set; }
		public string E { get; set; }
	}

	public class AppleJwksResponseModel
	{
		public AppleJwksModel[] Keys { get; set; }
	}
}
