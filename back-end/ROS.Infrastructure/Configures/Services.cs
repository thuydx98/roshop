using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using ROS.Services.Account.Commands.CreateAccount;
using ROS.Services.Account.Commands.VerifyAccount;
using ROS.Services.Product.Queries.GetListProduct;
using ROS.Services.Providers.Apple;
using ROS.Services.Providers.Facebook;
using ROS.Services.Providers.Google;
using ROS.Services.Providers.GrantValidator;
using ROS.Services.Providers.Repository;
using ROS.Services.Providers.UserProcessor;

namespace ROS.Infrastructure.Configures
{
	public static class Services
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			#region Account
			services.AddService<CreateAccountRequest, CreateAccountHandler>();
			services.AddService<VerifyAccountRequest, VerifyAccountHandler>();
			#endregion

			#region Products
			services.AddService<GetListProductRequest, GetListProductHandler>();
			#endregion

			#region Providers
			services.AddScoped<IProviderRepository, ProviderRepository>();
			services.AddScoped<IUserProcessor, UserProcessor>();
			services.AddScoped<IExtensionGrantValidator, ExternalGrantValidator>();
			services.AddScoped<IFacebookAuthProvider, FacebookAuthProvider>();
			services.AddScoped<IGoogleAuthProvider, GoogleAuthProvider>();
			services.AddScoped<IAppleAuthProvider, AppleAuthProvider>();
			#endregion

			return services;
		}
	}
}
