using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ROS.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
			//CreateHostBuilder(args).Build().UpdateSeedDataAsync().Result.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
