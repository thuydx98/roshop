using System;

namespace ROS.Common.Extensions
{
	public static class String
	{
		public static bool IsEmpty(this string s) => s == null || s.Trim().Length == 0;
		public static bool IsNotEmpty(this string s) => s != null && s.Trim().Length > 0;

		public static object ToEnum<TResult>(this string s) where TResult : System.Enum
		{
			if (s.IsNotEmpty() && System.Enum.TryParse(typeof(TResult), s, true, out object value))
			{
				if (System.Enum.IsDefined(typeof(TResult), value))
				{
					return value;
				}
			}

			return null;
		}

		public static string ToPascalWords(this string value)
		{
			char[] array = value.ToCharArray();
			// Handle the first letter in the string.
			if (array.Length >= 1)
			{
				if (char.IsLower(array[0]))
				{
					array[0] = char.ToUpper(array[0]);
				}
			}
			// Scan through the letters, checking for spaces.
			// Uppercase the lowercase letters following spaces.
			for (int i = 1; i < array.Length; i++)
			{
				if (array[i - 1] == ' ')
				{
					if (char.IsLower(array[i]))
					{
						array[i] = char.ToUpper(array[i]);
					}
				}
			}
			return new string(array);
		}

		private static readonly string[] VietnameseSigns = new string[]
		{
			"aAeEoOuUiIdDyY",
			"áàạảãâấầậẩẫăắằặẳẵ",
			"ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
			"éèẹẻẽêếềệểễ",
			"ÉÈẸẺẼÊẾỀỆỂỄ",
			"óòọỏõôốồộổỗơớờợởỡ",
			"ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
			"úùụủũưứừựửữ",
			"ÚÙỤỦŨƯỨỪỰỬỮ",
			"íìịỉĩ",
			"ÍÌỊỈĨ",
			"đ",
			"Đ",
			"ýỳỵỷỹ",
			"ÝỲỴỶỸ"
		};

		/// <summary>
		/// Remove diacritics in vietnamese for full-text search
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string RemoveDiacritics(this string text)
		{
			for (int i = 1; i < VietnameseSigns.Length; i++)
			{
				for (int j = 0; j < VietnameseSigns[i].Length; j++)
				{
					text = text.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
				}
			}

			return text;
		}

		public static byte[] ConvertToBase64(this string base64Url)
		{
			string padded = base64Url.Length % 4 == 0
				? base64Url
				: base64Url + "====".Substring(base64Url.Length % 4);
			string base64 = padded.Replace("_", "/")
								  .Replace("-", "+");

			return Convert.FromBase64String(base64);
		}
	}
}
