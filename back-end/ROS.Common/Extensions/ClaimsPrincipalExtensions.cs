using System.Security.Claims;

namespace ROS.Common.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string UserId(this ClaimsPrincipal principal) => principal.FindFirst(ClaimTypes.Sid)?.Value;
		public static string UserName(this ClaimsPrincipal principal) => principal.FindFirst(ClaimTypes.Name)?.Value;
	}
}
