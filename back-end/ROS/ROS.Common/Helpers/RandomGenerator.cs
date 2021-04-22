using System;
using System.Linq;

namespace ROS.Common.Helpers
{
	public static class RandomGenerator
	{
		/// <summary>
		/// Generate random code
		/// </summary>
		public static string GenerateCode(int length)
		{
			Random generator = new Random();
			var code = generator.Next(0, 1000000).ToString("D" + length);
			if (code.Distinct().Count() == 1)
			{
				code = GenerateCode(length);
			}

			return code;
		}
	}
}
