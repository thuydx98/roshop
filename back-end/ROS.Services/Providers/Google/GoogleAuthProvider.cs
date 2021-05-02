using Newtonsoft.Json.Linq;
using ROS.Common.Enums;
using ROS.Services.Providers.Models;
using ROS.Services.Providers.Repository;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ROS.Services.Providers.Google
{
	public class GoogleAuthProvider : IGoogleAuthProvider
	{
		private readonly IProviderRepository _providerRepository;
		private readonly HttpClient _httpClient;

		public GoogleAuthProvider(IProviderRepository providerRepository, HttpClient httpClient)
		{
			_providerRepository = providerRepository;
			_httpClient = httpClient;
		}

		public ProviderModel Provider => _providerRepository.GetProviders().FirstOrDefault(x => x.Name == ProviderType.Google);

		public async Task<ExternalUserModel> GetUserInfoAsync(string accessToken)
		{
			var result = await _httpClient.GetAsync(Provider.UserInfoEndPoint + $"?access_token={accessToken}");
			if (!result.IsSuccessStatusCode)
			{
				return null;
			}

			var content = await result.Content.ReadAsStringAsync();
			var infoObject = JObject.Parse(content);

			return new ExternalUserModel()
			{
				Id = infoObject["id"].ToString(),
				Email = infoObject["email"].ToString(),
				FirstName = infoObject["given_name"].ToString(),
				LastName = infoObject["family_name"].ToString(),
				FullName = infoObject["name"].ToString(),
				AvatarUrl = infoObject["picture"].ToString()
			};
		}
	}
}
