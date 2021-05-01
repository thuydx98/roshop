using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ROS.Common.Constants.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ROS.Infrastructure.Configures
{
	public static class Auth
	{
		public static IServiceCollection AddAuth(this IServiceCollection services)
		{
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.Authority = Environment.GetEnvironmentVariable("API_URL");
				options.RequireHttpsMetadata = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateAudience = false
				};
			});

			services.AddAuthorization(options =>
			{
				options.AddPolicy(Policies.API_SCOPE, policy =>
				{
					policy.RequireAuthenticatedUser();
					policy.RequireClaim("scope", ApiScopes.API);
				});
			});

			return services;
		}
	}
}
