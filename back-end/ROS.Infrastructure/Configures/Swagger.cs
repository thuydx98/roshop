using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ROS.Infrastructure.Configures
{
	public static class Swagger
	{
		public static IServiceCollection AddSwagger(this IServiceCollection services)
		{
			var API_URL = Environment.GetEnvironmentVariable("API_URL");

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "Rosen Shop", Version = "v1" });

				options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OAuth2,
					Flows = new OpenApiOAuthFlows
					{
						AuthorizationCode = new OpenApiOAuthFlow
						{
							AuthorizationUrl = new Uri(API_URL + "/connect/authorize"),
							TokenUrl = new Uri(API_URL + "/connect/token"),
							Scopes = new Dictionary<string, string>
							{
								{"api", "RoShop API - full access"}
							}
						}
					}
				});

				options.AddSecurityDefinition("AccountsOpenID", new OpenApiSecurityScheme
				{
					Type = SecuritySchemeType.OpenIdConnect,
					OpenIdConnectUrl = new Uri(API_URL + "/.well-known/openid-configuration"),
				});

				options.OperationFilter<AuthorizeCheckOperationFilter>();
			});

			return services;
		}

		public static IApplicationBuilder UseDeveloperSwagger(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(options =>
			{
				options.SwaggerEndpoint("/swagger/v1/swagger.json", "Rosen Shop v1");

				options.OAuthClientId("website");
				options.OAuthAppName("RoShop API - Swagger");
				options.OAuthUsePkce();
			});

			return app;
		}
	}

	class AuthorizeCheckOperationFilter : IOperationFilter
	{
		public void Apply(OpenApiOperation operation, OperationFilterContext context)
		{
			var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
							   context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();

			if (hasAuthorize)
			{
				operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
				operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

				operation.Security = new List<OpenApiSecurityRequirement>
				{
					new OpenApiSecurityRequirement
					{
						[new OpenApiSecurityScheme {Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "AccountsOpenID"}}] = new[] {"api"}
					}
				};
			}
		}
	}
}
