using ROS.Services.Providers.Models;

namespace ROS.Services.Providers.Repository
{
	public interface IProviderRepository
	{
		ProviderModel[] GetProviders();
	}
}
