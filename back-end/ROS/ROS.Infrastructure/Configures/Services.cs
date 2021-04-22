using ROS.Services.User.Queries;
using ROS.Services.User.Queries.GetUserInfo;
using Microsoft.Extensions.DependencyInjection;

namespace ROS.Infrastructure.Configures
{
	public static class Services
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddUserServices();

			return services;
		}

		private static void AddUserServices(this IServiceCollection services)
		{
			services.AddService<GetUserInfoQuery, GetUserInfoHandler>();
		}
	}
}
