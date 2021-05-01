using IdentityServer4.Services;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ROS.Data.Contexts.Application;
using ROS.Data.Contexts.Identity;
using ROS.Data.Entities;
using ROS.Services.Identity;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace MBP.Identity.Infrastructure.Configures
{
	public static class IdentityProvider
	{
		public static IServiceCollection AddIdentityProvider(this IServiceCollection services)
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var connectionString = Environment.GetEnvironmentVariable("CONFIGURATION_DATABASE_CONNECTION_STRING");
			var signingCredential = Environment.GetEnvironmentVariable("SIGNING_CREDENTIAL");
			var protectKeyPath = Environment.GetEnvironmentVariable("PROTECT_KEY_PATH");
			var tokenLifespanInMinutes = Environment.GetEnvironmentVariable("TOKEN_EXPIRE_TIME_IN_MINUTES");
			var tokenLifespan = TimeSpan.FromMinutes(double.Parse(tokenLifespanInMinutes));

			services.AddDbContext<ConfigurationContext>(options => options.UseNpgsql(connectionString));
			services.AddDbContext<PersistedGrantContext>(options => options.UseNpgsql(connectionString));

			services.AddTransient<IProfileService, ProfileService>();

			services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(protectKeyPath));

			services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = tokenLifespan);

			services.AddIdentity<UserEntity, RoleEntity>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 0;
				options.Password.RequiredUniqueChars = 0;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.Password.RequireDigit = false;
				options.Password.RequireNonAlphanumeric = false;
				options.User.AllowedUserNameCharacters = "abcdefghiıjklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+'#!/^%{}*";
			})
			.AddEntityFrameworkStores<ReadDbContext>()
			.AddSignInManager<SignInValidator<UserEntity>>()
			.AddDefaultTokenProviders();

			var serverBuilder = environment == "Development"
				? services.AddIdentityServer().AddDeveloperSigningCredential()
				: services.AddIdentityServer().AddSigningCredential(new X509Certificate2(signingCredential));

			serverBuilder.AddOperationalStore(options =>
			{
				options.EnableTokenCleanup = true;
				options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString);
			});

			serverBuilder.AddConfigurationStore(options =>
			{
				options.ConfigureDbContext = builder => builder.UseNpgsql(connectionString);
			});

			serverBuilder.AddAspNetIdentity<UserEntity>();

			return services;
		}
	}
}
