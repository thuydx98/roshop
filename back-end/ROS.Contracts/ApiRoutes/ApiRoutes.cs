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

		public static class Carts
		{
			private const string CONTROLLER_URI = ROOT + "/carts";
			public const string GET = CONTROLLER_URI + "/my-cart";
			public const string UPDATE = CONTROLLER_URI + "/my-cart";
			public const string SYNC = CONTROLLER_URI + "/my-cart/sync";
		}

		public static class Products
		{
			private const string CONTROLLER_URI = ROOT + "/products";
			public const string GET_LIST = CONTROLLER_URI;
			public const string GET = CONTROLLER_URI + "/{id}";
		}
	}
}
