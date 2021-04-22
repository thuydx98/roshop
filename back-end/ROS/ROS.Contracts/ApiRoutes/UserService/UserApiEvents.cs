using ROS.Common.ApiResponse;
using ROS.Common.Extensions;
using ROS.Contracts.ApiRoutes.UserService.ViewModels;
using System;
using System.Threading.Tasks;

namespace ROS.Contracts.ApiRoutes.UserService
{
	public static class UserApiEvents
	{
		public static async Task<ProfileViewModel> GetUserProfileAsync(this Lazy<System.Net.Http.HttpClient> lazyHttpClient, string userId)
		{
			var endpoint = UserApiRoutes.Users.GET_USER_PROFILE.Replace("{id}", userId);
			var content = await lazyHttpClient.Value.GetAsync<ApiJsonResult<ProfileViewModel>>(endpoint);

			return content.Result;
		}
	}
}
