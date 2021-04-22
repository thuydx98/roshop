using ROS.Contracts.User;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ROS.Contracts.Middlewares.Authorization.Configures
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddAuthorize(this IServiceCollection services)
		{
			services.AddHttpContextAccessor();
			services.AddScoped<Lazy<HttpClient>>();
			services.AddScoped<IAuthorizationHandler, PermissionHandler>();

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

			foreach (var permission in Permisison.ListPermission)
			{
				services.AddAuthorization(options =>
					options.AddPolicy(permission, policy =>
						policy.Requirements.Add(new PermissionRequirement(permission))));
			}

			return services;
		}
	}
}
