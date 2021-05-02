using ROS.Common.Enums;
using ROS.Common.Extensions;
using ROS.Services.Providers.Models;
using ROS.Services.Providers.Repository;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ROS.Services.Providers.Apple
{
	public class AppleAuthProvider : IAppleAuthProvider
	{
		private readonly IProviderRepository _providerRepository;
		private readonly HttpClient _httpClient;

		public AppleAuthProvider(IProviderRepository providerRepository, HttpClient httpClient)
		{
			_providerRepository = providerRepository;
			_httpClient = httpClient;
		}

		public ProviderModel Provider => _providerRepository.GetProviders().FirstOrDefault(x => x.Name == ProviderType.Apple);

		public async Task<ExternalUserModel> GetUserInfoAsync(string accessToken)
		{
			if (Provider == null)
			{
				throw new ArgumentNullException(nameof(Provider));
			}

			var keys = await GetJwksAsync();
			var tokens = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

			var jwks = keys.Where(n => n.Kid == tokens.Header.Kid && n.Alg == tokens.Header.Alg).FirstOrDefault();
			if (jwks == null)
			{
				return null;
			}

			var isValidToken = ValidateToken(accessToken, jwks, tokens);
			if (!isValidToken)
			{
				return null;
			}

			return new ExternalUserModel()
			{
				Id = tokens.Payload.Sub,
				Email = tokens.Payload["email"]?.ToString(),
			};
		}

		private async Task<AppleJwksModel[]> GetJwksAsync()
		{
			var response = await _httpClient.GetAsync(Provider.PublicKeyUri);
			if (!response.IsSuccessStatusCode)
			{
				return Array.Empty<AppleJwksModel>();
			}

			var responseContent = await response.Content.ReadAsStringAsync();
			var jwks = JsonSerializer.Deserialize<AppleJwksResponseModel>(responseContent, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			});

			return jwks.Keys;
		}

		private bool ValidateToken(string accessToken, AppleJwksModel jwks, JwtSecurityToken tokens)
		{
			var APP_BUNDLE_ID = Environment.GetEnvironmentVariable("APP_BUNDLE_ID");
			var tokenParts = accessToken.Split('.');
			var rsa = new RSACryptoServiceProvider();
			rsa.ImportParameters(new RSAParameters()
			{
				Modulus = jwks.N.ConvertToBase64(),
				Exponent = jwks.E.ConvertToBase64(),
			});

			var sha256 = SHA256.Create();
			var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes($"{tokenParts[0]}.{tokenParts[1]}"));

			var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsaDeformatter.SetHashAlgorithm("SHA256");

			var isValidSignature = rsaDeformatter.VerifySignature(hash, tokenParts[2].ConvertToBase64());
			if (!isValidSignature)
			{
				return false;
			}

			if (tokens.Payload.Iss != Provider.Iss) return false;
			if (tokens.Payload.Aud.SingleOrDefault() != APP_BUNDLE_ID) return false;
			if (!tokens.Payload.Exp.HasValue || tokens.Payload.Exp < DateTimeOffset.UtcNow.ToUnixTimeSeconds()) return false;

			return true;
		}
	}
}
