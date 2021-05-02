using ROS.Services.Providers.Models;

namespace ROS.Services.Providers.Google
{
	public interface IGoogleAuthProvider : IExternalAuthProvider
	{
		ProviderModel Provider { get; }
	}
}
