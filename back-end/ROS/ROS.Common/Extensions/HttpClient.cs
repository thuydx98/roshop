using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ROS.Common.Enums;

namespace ROS.Common.Extensions
{
	public static class HttpClient
	{
		public static async Task<TResult> ReadContentAsync<TResult>(this HttpResponseMessage response)
		{
			var content = await response.Content.ReadAsStringAsync();
			var result = JsonSerializer.Deserialize<TResult>(content, new JsonSerializerOptions
			{
				PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
			});

			return result;
		}

		public static async Task<TResult> GetAsync<TResult>(this System.Net.Http.HttpClient httpClient, string requestUri)
		{
			var response = await httpClient.GetAsync(requestUri);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(JsonSerializer.Serialize(response));
			}

			return await response.ReadContentAsync<TResult>();
		}

		public static async Task<HttpResponseMessage> PostAsJsonAsync(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var httpContent = ConvertToStringContent(data);
			return await httpClient.PostAsync(url, httpContent);
		}

		public static async Task<TResult> PostAsJsonAsync<TResult>(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var response = await httpClient.PostAsJsonAsync(url, data);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(JsonSerializer.Serialize(response));
			}

			return await response.ReadContentAsync<TResult>();
		}

		public static async Task<HttpResponseMessage> PostAsFormAsync(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var httpContent = await ConvertToFormDataAsync(data);
			return await httpClient.PostAsync(url, httpContent);
		}

		public static async Task<TResult> PostAsFormAsync<TResult>(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var response = await httpClient.PostAsFormAsync(url, data);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(JsonSerializer.Serialize(response));
			}

			return await response.ReadContentAsync<TResult>();
		}

		public static async Task<HttpResponseMessage> PutAsJsonAsync(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var httpContent = ConvertToStringContent(data);
			return await httpClient.PutAsync(url, httpContent);
		}

		public static async Task<TResult> PutAsJsonAsync<TResult>(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var response = await httpClient.PutAsJsonAsync(url, data);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(JsonSerializer.Serialize(response));
			}

			return await response.ReadContentAsync<TResult>();
		}

		public static async Task<HttpResponseMessage> PutAsFormAsync(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var httpContent = await ConvertToFormDataAsync(data);
			return await httpClient.PutAsync(url, httpContent);
		}

		public static async Task<TResult> PutAsFormAsync<TResult>(this System.Net.Http.HttpClient httpClient, string url, object data = null)
		{
			var response = await httpClient.PutAsFormAsync(url, data);
			if (!response.IsSuccessStatusCode)
			{
				throw new HttpRequestException(JsonSerializer.Serialize(response));
			}

			return await response.ReadContentAsync<TResult>();
		}

		private static StringContent ConvertToStringContent(object data)
		{
			if (data == null) return null;
			var content = JsonSerializer.Serialize(data);
			var mediaType = HeaderMediaType.JSON.GetDescription();

			return new StringContent(content, Encoding.UTF8, mediaType);
		}

		private static async Task<MultipartFormDataContent> ConvertToFormDataAsync(object obj)
		{
			if (obj == null) return null;

			var form = new MultipartFormDataContent();
			foreach (var item in obj.GetType().GetProperties())
			{
				if (item.GetValue(obj, null) != null)
				{
					if (item.PropertyType == typeof(Microsoft.AspNetCore.Http.IFormFile))
					{
						var file = (Microsoft.AspNetCore.Http.IFormFile)item.GetValue(obj, null);
						var bytes = await file.GetBytes();
						form.Add(new ByteArrayContent((byte[])bytes, 0, (int)bytes.Length), item.Name, file.FileName);
					}
					else
					{
						form.Add(new StringContent(item.GetValue(obj, null)?.ToString()), item.Name);
					}
				}
			}

			return form;
		}
	}
}
