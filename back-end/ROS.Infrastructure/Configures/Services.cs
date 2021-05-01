using Microsoft.Extensions.DependencyInjection;
using ROS.Services.Product.Queries.GetListProduct;

namespace ROS.Infrastructure.Configures
{
	public static class Services
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			#region Products
			services.AddService<GetListProductQuery, GetListProductHandler>();
			#endregion

			return services;
		}
	}
}
