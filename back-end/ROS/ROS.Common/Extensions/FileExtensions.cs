using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ROS.Common.Extensions
{
	public static class FileExtensions
	{
		public static async Task<string> ReadFileContentAsync(IList<string> paths)
		{
			paths.Insert(0, "wwwroot");
			paths.Insert(0, Directory.GetCurrentDirectory());

			var file = Path.Combine(paths.Select(path => path).ToArray());
			FileStream fileStream = new FileStream(file, FileMode.Open);

			using (StreamReader reader = new StreamReader(fileStream))
			{
				return await reader.ReadToEndAsync();
			}
		}
	}
}
