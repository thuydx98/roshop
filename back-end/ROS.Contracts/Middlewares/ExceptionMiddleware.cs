using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Text.Json;
using ROS.Common.ApiResponse;
using ROS.Common.Enums;
using ROS.Common.Extensions;
using ROS.Common.ApiResponse.ErrorResult;

namespace ROS.Contracts.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(context, ex);
			}
		}

		private Task HandleExceptionAsync(HttpContext context, Exception ex)
		{
			_logger.LogError(ex.Message);

			var response = context.Response;
			var statusCode = (int)HttpCode.InternalServerError;
			var result = JsonSerializer.Serialize(new ApiJsonResult<object>(statusCode, ex.Message), new JsonSerializerOptions()
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			});

			response.OnStarting(async () =>
			{
				response.StatusCode = statusCode;
				response.ContentType = HeaderMediaType.JSON.GetDescription();
				await response.WriteAsync(result);
			});

			return Task.CompletedTask;
		}
	}
}
