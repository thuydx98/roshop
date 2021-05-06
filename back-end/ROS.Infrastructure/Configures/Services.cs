using IdentityServer4.Validation;
using Microsoft.Extensions.DependencyInjection;
using ROS.Services.Account.Commands.CreateAccount;
using ROS.Services.Account.Commands.VerifyAccount;
using ROS.Services.Cart.Commands.UpdateCart;
using ROS.Services.Cart.Queries.GetCart;
using ROS.Services.Order.Commands.CreateOrder;
using ROS.Services.Order.Commands.UpdateOrderStatus;
using ROS.Services.Order.Queries.GetOrderById;
using ROS.Services.Order.Queries.GetPagingListOrder;
using ROS.Services.Product.Queries.GetListProduct;
using ROS.Services.Product.Queries.GetProduct;
using ROS.Services.Product.Queries.GetProductByIds;
using ROS.Services.Providers.Apple;
using ROS.Services.Providers.Facebook;
using ROS.Services.Providers.Google;
using ROS.Services.Providers.GrantValidator;
using ROS.Services.Providers.Repository;
using ROS.Services.Providers.UserProcessor;
using System;

namespace ROS.Infrastructure.Configures
{
	public static class Services
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = Environment.GetEnvironmentVariable("REDIS_HOST");
			});

			#region Account
			services.AddService<CreateAccountRequest, CreateAccountHandler>();
			services.AddService<VerifyAccountRequest, VerifyAccountHandler>();
			#endregion

			#region Products
			services.AddService<GetListProductRequest, GetListProductHandler>();
			services.AddService<GetProductRequest, GetProductHandler>();
			services.AddService<GetProductByIdsRequest, GetProductByIdsHandler>();
			#endregion

			#region Carts
			services.AddService<GetCartRequest, GetCartHandler>();
			services.AddService<UpdateCartRequest, UpdateCartHandler>();
			#endregion

			#region Orders
			services.AddService<CreateOrderRequest, CreateOrderHandler>();
			services.AddService<UpdateOrderStatusRequest, UpdateOrderStatusHandler>();
			services.AddService<GetOrderByIdRequest, GetOrderByIdHandler>();
			services.AddService<GetPagingListOrderRequest, GetPagingListOrderHandler>();
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
