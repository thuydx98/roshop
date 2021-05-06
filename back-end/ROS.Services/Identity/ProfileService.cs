using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using ROS.Common.Extensions;
using ROS.Data.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ROS.Services.Identity
{
	public class ProfileService : IProfileService
	{
		private readonly IUserClaimsPrincipalFactory<UserEntity> _claimsFactory;
		private readonly UserManager<UserEntity> _userManager;

		public ProfileService(IUserClaimsPrincipalFactory<UserEntity> claimsFactory, UserManager<UserEntity> userManager)
		{
			_claimsFactory = claimsFactory;
			_userManager = userManager;
		}

		public async Task GetProfileDataAsync(ProfileDataRequestContext context)
		{
			var sub = context.Subject.GetSubjectId();
			var user = await _userManager.FindByIdAsync(sub);
			var principal = await _claimsFactory.CreateAsync(user);
			var claims = principal.Claims.ToList();
			var roles = await _userManager.GetRolesAsync(user);

			claims = claims.Where(c => context.RequestedClaimTypes.Contains(c.Type)).ToList();

			if (user.FullName.IsNotEmpty())
			{
				claims.Add(new Claim(JwtClaimTypes.Name, user.FullName));
			}
			if (user.Email.IsNotEmpty())
			{
				claims.Add(new Claim(JwtClaimTypes.Email, user.Email));
			}

			foreach (var role in roles)
			{
				claims.Add(new Claim(JwtClaimTypes.Role, role));
			}

			context.IssuedClaims = claims;
		}

		public Task IsActiveAsync(IsActiveContext context)
		{
			context.IsActive = true;

			return Task.CompletedTask;
		}
	}
}
