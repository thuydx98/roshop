using System;

namespace ROS.Contracts.ApiRoutes.UserService
{
	public static class UserApiRoutes
	{
		private const string ROOT = "api";
		private static readonly string _baseUrl = Environment.GetEnvironmentVariable("USER_SERVICE_URL");

		public static string GetUserApiUrl(this string apiRoute) => string.Concat(_baseUrl, "/", apiRoute);

		public static class Users
		{
			private const string CONTROLLER_URI = ROOT + "/users";

			public const string GET_PAGING_LIST = CONTROLLER_URI;
			public const string GET = CONTROLLER_URI + "/{id}";
			public const string ADD = CONTROLLER_URI;
			public const string EDIT = CONTROLLER_URI + "/{id}";

			public const string GET_USER_PROFILE = CONTROLLER_URI + "/{id}/profile";
		}
	}
}
