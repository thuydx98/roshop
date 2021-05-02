using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using ROS.Common.Enums;
using ROS.Common.Extensions;
using ROS.Data.Contexts.Application;
using ROS.Data.Entities;
using ROS.Services.Providers.Apple;
using ROS.Services.Providers.Facebook;
using ROS.Services.Providers.Google;
using ROS.Services.Providers.Repository;
using ROS.Services.Providers.UserProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROS.Services.Providers.GrantValidator
{
	public class ExternalGrantValidator : IExtensionGrantValidator
	{
		public string GrantType => "external";
		private readonly Dictionary<ProviderType, IExternalAuthProvider> _providers;
		private readonly UserManager<UserEntity> _userManager;
		private readonly IProviderRepository _providerRepository;
		private readonly IFacebookAuthProvider _facebookAuthProvider;
		private readonly IGoogleAuthProvider _googleAuthProvider;
		private readonly IAppleAuthProvider _appleAuthProvider;
		private readonly IUserProcessor _userProcessor;

		public ExternalGrantValidator(
			UserManager<UserEntity> userManager,
			IServiceProvider serviceProvider,
			IProviderRepository providerRepository,
			IFacebookAuthProvider facebookAuthProvider,
			IGoogleAuthProvider googleAuthProvider,
			IAppleAuthProvider appleAuthProvider,
			IUserProcessor userProcessorr)
		{
			_userManager = userManager;
			_providerRepository = providerRepository;
			_facebookAuthProvider = facebookAuthProvider;
			_googleAuthProvider = googleAuthProvider;
			_appleAuthProvider = appleAuthProvider;
			_userProcessor = userProcessorr;

			_providers = new Dictionary<ProviderType, IExternalAuthProvider>
			{
				 {ProviderType.Facebook, _facebookAuthProvider},
				 {ProviderType.Google, _googleAuthProvider},
				 {ProviderType.Apple, _appleAuthProvider},
			};
		}

		public async Task ValidateAsync(ExtensionGrantValidationContext context)
		{
			var provider = context.Request.Raw.Get("provider");
			if (provider.IsEmpty())
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid provider");
				return;
			}

			var providerType = (ProviderType)Enum.Parse(typeof(ProviderType), provider, true);
			if (!Enum.IsDefined(typeof(ProviderType), providerType))
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid provider");
				return;
			}

			var token = context.Request.Raw.Get("external_token");
			if (token.IsEmpty())
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid external token");
				return;
			}

			var userInfo = await _providers[providerType].GetUserInfoAsync(token);
			if (userInfo == null)
			{
				context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "couldn't retrieve user info from specified provider, please make sure that access token is not expired.");
				return;
			}

			context.Result = await _userProcessor.ExecuteAsync(userInfo, provider);
		}
	}
}
