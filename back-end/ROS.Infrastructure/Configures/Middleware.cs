using ROS.Contracts.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ROS.Infrastructure.Configures
{
	public static class Middleware
	{
		public static IApplicationBuilder UseMiddlewares(this IApplicationBuilder builder)
		{
			builder.UseMiddleware<ExceptionMiddleware>();

			return builder;
		}
	}
}
