using ROS.Common.Enums;
using ROS.Services.Providers.Models;

namespace ROS.Services.Providers.Repository
{
	public class ProviderRepository : IProviderRepository
	{
		public ProviderModel[] GetProviders() => new ProviderModel[]
		{
			new ProviderModel
			{
				ProviderId = 1,
				Name = ProviderType.Facebook,
				UserInfoEndPoint = "https://graph.facebook.com/v2.8/me"
			},
			new ProviderModel
			{
				ProviderId = 2,
				Name = ProviderType.Google,
				UserInfoEndPoint = "https://www.googleapis.com/oauth2/v2/userinfo"
			},
			new ProviderModel
			{
				ProviderId = 3,
				Name = ProviderType.Apple,
				Iss = "https://appleid.apple.com",
				PublicKeyUri = "https://appleid.apple.com/auth/keys",
			},
		};
	}
}
