using ROS.Common.Enums;

namespace ROS.Services.Providers.Models
{
	public class ProviderModel
	{
		public int ProviderId { get; set; }
		public ProviderType Name { get; set; }
		public string UserInfoEndPoint { get; set; }
		public string Iss { get; set; }
		public string PublicKeyUri { get; set; }
	}
}
