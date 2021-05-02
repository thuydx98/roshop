using System;

namespace ROS.Contracts.ApiRoutes
{
	public static class ApiRoutes
	{
		private const string ROOT = "api";

		public static class Account
		{
			private const string CONTROLLER_URI = ROOT + "/account";
			public const string VERIFY = CONTROLLER_URI + "/verify";
		}

		public static class Auth
		{
			private const string CONTROLLER_URI = ROOT + "/auth";
			public const string REGISTER = CONTROLLER_URI + "/register";
		}

		public static class Products
		{
			private const string CONTROLLER_URI = ROOT + "/products";
			public const string GET_LIST = CONTROLLER_URI;
			public const string GET = CONTROLLER_URI + "/{id}";
		}
	}
}
