using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ROS.Contracts.Configures;
using ROS.Infrastructure.Configures;
using ROS.Infrastructure.Mapper;
using System.IO;
using System.Text.Json.Serialization;

namespace ROS.Api
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMediator();
			services.AddAutoMapper();
			services.AddUnitOfWork();
			services.AddServices();
			services.AddSwagger();

			services.AddHealthChecks();
			services.AddControllers().AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDeveloperSwagger();
			}

			app.UseDefaultFiles();
			app.UseStaticFiles();
			app.Use(async (context, next) =>
			{
				await next();

				if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
				{
					context.Request.Path = "/index.html";
					await next();
				}
			});


			app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			app.UseRouting();
			app.UseMiddlewares();
			app.UseLocalization().UseHealthChecks();

			app.UseEndpoints(endpoints => endpoints.MapControllers());
		}
	}
}