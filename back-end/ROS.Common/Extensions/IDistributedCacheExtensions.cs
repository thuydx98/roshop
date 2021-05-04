using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;
using System.Text.Json;
using System;

namespace ROS.Common.Extensions
{
	public static class IDistributedCacheExtensions
	{
		public static async Task<T> GetCacheValueAsync<T>(this IDistributedCache cache, string key) where T : class
		{
			string result = await cache.GetStringAsync(key);
			if (result.IsEmpty())
			{
				return null;
			}

			return JsonSerializer.Deserialize<T>(result);
		}

		public static async Task SetCacheValueAsync<T>(
			this IDistributedCache cache, 
			string key, 
			T value,
			int? absoluteExpireTimeInSeconds,
			int slidingExpireTimeInSeconds = 60) where T : class
		{
			var cacheEntryOptions = new DistributedCacheEntryOptions();

			// Remove item from cache after duration
			if (absoluteExpireTimeInSeconds.HasValue)
			{
				cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(absoluteExpireTimeInSeconds.Value);
			}

			// Remove item from cache if unsued for the duration
			cacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(slidingExpireTimeInSeconds);

			var result = JsonSerializer.Serialize<T>(value);

			await cache.SetStringAsync(key, result);
		}
	}
}
