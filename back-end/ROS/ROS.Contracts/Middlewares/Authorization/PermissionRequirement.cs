using Microsoft.AspNetCore.Authorization;

namespace ROS.Contracts.Middlewares.Authorization
{
	public class PermissionRequirement : IAuthorizationRequirement
	{
		public string Permission { get; }
		public PermissionRequirement(string permission)
		{
			Permission = permission;
		}
	}
}
