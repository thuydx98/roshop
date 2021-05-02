using ROS.Services.Providers.Models;

namespace ROS.Services.Providers.Apple
{
	public interface IAppleAuthProvider : IExternalAuthProvider
	{
		ProviderModel Provider { get; }
	}
}
