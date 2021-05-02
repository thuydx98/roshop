using IdentityServer4.Validation;
using ROS.Services.Providers.Models;
using System.Threading.Tasks;

namespace ROS.Services.Providers.UserProcessor
{
	public interface IUserProcessor
	{
		Task<GrantValidationResult> ExecuteAsync(ExternalUserModel userInfo, string provider);
	}
}
