using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ROS.Data.Entities;
using System.Threading.Tasks;

namespace ROS.Services.Identity
{
	public class SignInValidator<TUser> : SignInManager<UserEntity>
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public SignInValidator(
			UserManager<UserEntity> userManager,
			IHttpContextAccessor contextAccessor,
			IUserClaimsPrincipalFactory<UserEntity> claimsFactory,
			IOptions<IdentityOptions> optionsAccessor,
			ILogger<SignInManager<UserEntity>> logger,
			IAuthenticationSchemeProvider schemes,
			IUserConfirmation<UserEntity> confirmation) :
			base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
		{
			_contextAccessor = contextAccessor;
		}


		/// <summary>
		/// Custom validate when sign in
		/// </summary>
		public override Task<bool> CanSignInAsync(UserEntity user)
		{
			var canSignIn = user.EmailConfirmed;
			return Task.FromResult(canSignIn);
		}
	}
}