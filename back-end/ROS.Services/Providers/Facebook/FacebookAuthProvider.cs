using Newtonsoft.Json.Linq;
using ROS.Common.Enums;
using ROS.Services.Providers.Models;
using ROS.Services.Providers.Repository;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ROS.Services.Providers.Facebook
{
	public class FacebookAuthProvider : IFacebookAuthProvider
	{
		private readonly IProviderRepository _providerRepository;
		private readonly HttpClient _httpClient;

		public FacebookAuthProvider(
			IProviderRepository providerRepository,
			HttpClient httpClient)
		{
			_providerRepository = providerRepository;
			_httpClient = httpClient;
		}

		public ProviderModel Provider => _providerRepository.GetProviders().FirstOrDefault(x => x.Name == ProviderType.Facebook);

		public async Task<ExternalUserModel> GetUserInfoAsync(string accessToken)
		{
			if (Provider == null)
			{
				throw new ArgumentNullException(nameof(Provider));
			}

			var fields = "id,email,name,first_name,last_name,middle_name,picture";
			var result = await _httpClient.GetAsync(Provider.UserInfoEndPoint + $"?fields={fields}&access_token={accessToken}");

			if (!result.IsSuccessStatusCode)
			{
				return null;
			}

			var content = await result.Content.ReadAsStringAsync();
			var infoObject = JObject.Parse(content);

			return new ExternalUserModel()
			{
				Id = infoObject["id"].ToString(),
				Email = infoObject["email"]?.ToString(),
				FirstName = infoObject["first_name"]?.ToString(),
				LastName = infoObject["last_name"]?.ToString(),
				FullName = infoObject["name"]?.ToString(),
				AvatarUrl = infoObject["picture"]["data"]["url"]?.ToString()
			};
		}
	}
}
