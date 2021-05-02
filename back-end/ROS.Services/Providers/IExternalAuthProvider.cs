using ROS.Services.Providers.Models;
using System.Threading.Tasks;

namespace ROS.Services.Providers
{
	public interface IExternalAuthProvider
	{
		Task<ExternalUserModel> GetUserInfoAsync(string accessToken);
	}
}
