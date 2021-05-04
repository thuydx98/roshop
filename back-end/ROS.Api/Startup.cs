using MBP.Identity.Infrastructure.Configures;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ROS.Common.Constants.Identity;
using ROS.Contracts.Configures;
using ROS.Infrastructure.Configures;
using ROS.Infrastructure.Mapper;
using ROS.Mail;
using System.Text.Json.Serialization;

namespace ROS.Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddUnitOfWork();   // Keep when migrate database

			services.AddMediator();
			services.AddAutoMapper();
			services.AddIdentityProvider().AddAuth();
			services.AddHttpClient().AddServices();
			services.AddSwagger();
			services.AddMailService();

			services.AddHealthChecks();
			services.AddResponseCaching();
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperSwagger();
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();

			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseRouting();
			app.UseMiddlewares();
			app.UseIdentityServer();
			app.UseLocalization().UseHealthChecks();
			app.UseResponseCaching();

			app.UseAuthentication().UseAuthorization();

			app.UseEndpoints(endpoints => endpoints.MapControllers().RequireAuthorization(Policies.API_SCOPE));
		}
	}
}
