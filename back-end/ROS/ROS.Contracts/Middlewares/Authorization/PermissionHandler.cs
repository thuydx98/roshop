using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Common.Constants;
using ROS.Common.Enums;
using ROS.Common.Extensions;
using ROS.Contracts.ApiRoutes.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ROS.Contracts.Middlewares.Authorization
{
	public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly Lazy<System.Net.Http.HttpClient> _lazyHttpClient;

		public PermissionHandler(
			IHttpContextAccessor httpContextAccessor,
			Lazy<System.Net.Http.HttpClient> lazyHttpClient)
		{
			_httpContextAccessor = httpContextAccessor;
			_lazyHttpClient = lazyHttpClient;
		}

		/// <summary>
		/// Authorize permission when request to endpoint
		/// </summary>
		/// <param name="context"></param>
		/// <param name="requirement">Permission</param>
		/// <returns></returns>
		protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
		{
			// Allow internal call by access path api
			if (_httpContextAccessor.HttpContext.Request.Path.Value.Contains("/api"))
			{
				context.Succeed(requirement);
				return;
			}

			var userId = _httpContextAccessor.HttpContext.Request.Headers[HeaderInfo.USER_ID].ToString();
			if (userId.IsEmpty())
			{
				HandleFailResult(HttpCode.Unauthorized);
				return;
			}

			var user = await _lazyHttpClient.GetUserProfileAsync(userId);
			if (user == null)
			{
				HandleFailResult(HttpCode.Unauthorized);
				return;
			}

			var hasPermisison = user.Permissions.Contains(requirement.Permission);
			if (!hasPermisison)
			{
				HandleFailResult(HttpCode.Forbidden);
				return;
			}

			context.Succeed(requirement);
		}

		private void HandleFailResult(HttpCode httpCode)
		{
			var response = _httpContextAccessor.HttpContext.Response;
			var content = new ApiJsonResult<object>((int)httpCode, httpCode.GetDescription());
			var message = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(content, new JsonSerializerOptions()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			}));

			response.OnStarting(async () =>
			{
				response.StatusCode = (int)httpCode;
				response.ContentType = HeaderMediaType.JSON.GetDescription();
				await response.Body.WriteAsync(message, 0, message.Length);
			});
		}
	}
}

