using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using ROS.Data.Entities;
using ROS.Services.Providers.Models;
using System;
using System.Threading.Tasks;

namespace ROS.Services.Providers.UserProcessor
{
	public class UserProcessor : IUserProcessor
	{
		private readonly UserManager<UserEntity> _userManager;
		public UserProcessor(UserManager<UserEntity> userManager)
		{
			_userManager = userManager;
		}

		public async Task<GrantValidationResult> ExecuteAsync(ExternalUserModel info, string provider)
		{
			var user = await _userManager.FindByLoginAsync(provider, info.Id);
			if (user == null)
			{
				user = new UserEntity
				{
					Email = info.Email,
					UserName = info.Id,
					FullName = info.FullName ?? $"{info.LastName} {info.FirstName}",
					AvatarUrl = info.AvatarUrl,
					CreatedAt = DateTime.UtcNow
				};

				await _userManager.CreateAsync(user);
			}

			var claims = await _userManager.GetClaimsAsync(user);
			await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, info.Id, provider));

			return new GrantValidationResult(user.Id.ToString(), provider, claims, provider, null);
		}
	}
}
