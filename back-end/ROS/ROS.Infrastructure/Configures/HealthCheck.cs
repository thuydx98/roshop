using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace ROS.Infrastructure.Configures
{
	public static class HealthCheck
	{
		public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
		{
			app.UseHealthChecks("/", new HealthCheckOptions
			{
				ResponseWriter = async (context, report) =>
				{
					var response = new object { };
					context.Response.ContentType = "application/json";
					await context.Response.WriteAsync(JsonSerializer.Serialize(response));
				}
			});

			return app;
		}
	}
}
