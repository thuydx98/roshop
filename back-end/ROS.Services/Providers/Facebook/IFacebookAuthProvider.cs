using ROS.Services.Providers.Models;

namespace ROS.Services.Providers.Facebook
{
	public interface IFacebookAuthProvider : IExternalAuthProvider
	{
		ProviderModel Provider { get; }
	}
}
