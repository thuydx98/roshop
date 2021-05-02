using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Common.Extensions;
using ROS.Common.Helpers;
using ROS.Common.Mail;
using ROS.Data.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Account.Commands.CreateAccount
{
	public class CreateAccountHandler : IRequestHandler<CreateAccountRequest, ApiResult>
	{
		private readonly UserManager<UserEntity> _userManager;
		private readonly Lazy<IMailService> _lazyMailService;
		private readonly ILogger _logger;

		public CreateAccountHandler(
			Lazy<IMailService> lazyMailService,
			UserManager<UserEntity> userManager,
			ILogger<CreateAccountHandler> logger)
		{
			_lazyMailService = lazyMailService;
			_userManager = userManager;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(CreateAccountRequest request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.Email.IsEmpty() || request.Password.IsEmpty())
				{
					return ApiResult.Failed(HttpCode.BadRequest);
				}

				var user = await _userManager.FindByNameAsync(request.Email);

				if (user != null)
				{
					if (user.EmailConfirmed)
					{
						return ApiResult.Failed(ErrorCode.EXISTED_USER);
					}

					await _userManager.RemovePasswordAsync(user);
					await _userManager.AddPasswordAsync(user, request.Password);
				}
				else
				{
					user = new UserEntity()
					{
						UserName = request.Email,
						Email = request.Email,
						CreatedAt = DateTime.UtcNow,
					};

					await _userManager.CreateAsync(user, request.Password);
					await _userManager.SetLockoutEnabledAsync(user, false);
				}

				await CreateActivationCodeAsync(user);

				_ = SendVerifyCodeAsync(user);

				return ApiResult.Succeeded();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}

		private async Task CreateActivationCodeAsync(UserEntity user)
		{
			var expireTime = int.Parse(Environment.GetEnvironmentVariable("TOKEN_EXPIRE_TIME_IN_HOURS"));

			user.ActivationCode = RandomGenerator.GenerateCode(6);
			user.CodeExpireAt = DateTime.UtcNow.AddHours(expireTime);

			await _userManager.UpdateAsync(user);
		}

		private async Task SendVerifyCodeAsync(UserEntity user)
		{
			try
			{
				var expireTime = Environment.GetEnvironmentVariable("TOKEN_EXPIRE_TIME_IN_HOURS");

				var paths = new List<string> { "templates", "verify-email.html" };
				var fileContent = await FileExtensions.ReadFileContentAsync(paths);
				var mailMessage = new MailMessage()
				{
					To = user.Email,
					Subject = "RoShop - Verify account",
					HtmlMessage = fileContent
						   .Replace("{verifyCode}", user.ActivationCode)
						   .Replace("{expireTime}", expireTime)
				};

				await _lazyMailService.Value.SendAsync(mailMessage);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
			}
		}
	}
}
