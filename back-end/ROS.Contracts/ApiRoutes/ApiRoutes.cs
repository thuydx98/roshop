using System;

namespace ROS.Contracts.ApiRoutes
{
	public static class ApiRoutes
	{
		private const string ROOT = "api";

		public static class Products
		{
			private const string CONTROLLER_URI = ROOT + "/products";

			public const string GET_LIST = CONTROLLER_URI;
			public const string GET = CONTROLLER_URI + "/{id}";
		}
	}
}
