using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using ROS.Common.ApiResponse;
using ROS.Common.ApiResponse.ErrorResult;
using ROS.Common.Extensions;
using ROS.Data.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ROS.Services.Account.Commands.VerifyAccount
{
	public class VerifyAccountHandler : IRequestHandler<VerifyAccountRequest, ApiResult>
	{
		private readonly UserManager<UserEntity> _userManager;
		private readonly ILogger _logger;

		public VerifyAccountHandler(UserManager<UserEntity> userManager, ILogger<VerifyAccountHandler> logger)
		{
			_userManager = userManager;
			_logger = logger;
		}

		public async Task<ApiResult> Handle(VerifyAccountRequest request, CancellationToken cancellationToken)
		{
			try
			{
				if (request.Email.IsEmpty() || request.Password.IsEmpty() || request.VerifyCode.IsEmpty())
				{
					return ApiResult.Failed(HttpCode.BadRequest);
				}

				var user = await _userManager.FindByNameAsync(request.Email);
				if (user == null)
				{
					return ApiResult.Failed(HttpCode.Notfound);
				}

				var isMatchPassword = await _userManager.CheckPasswordAsync(user, request.Password);
				if (!isMatchPassword)
				{
					return ApiResult.Failed(HttpCode.Notfound);
				}

				if (user.EmailConfirmed)
				{
					return ApiResult.Failed(ErrorCode.VERIFIED_ACCOUNT);
				}

				if (user.ActivationCode != request.VerifyCode)
				{
					return ApiResult.Failed(ErrorCode.INVALID_VERIFY_CODE);
				}

				if (user.CodeExpireAt < DateTime.UtcNow)
				{
					return ApiResult.Failed(ErrorCode.EXPIRED_CODE);
				}

				user.ActivationCode = null;
				user.CodeExpireAt = null;
				user.EmailConfirmed = true;

				await _userManager.UpdateAsync(user);

				return ApiResult.Succeeded();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.ToString());
				return ApiResult.Failed(HttpCode.InternalServerError);
			}
		}
	}
}
