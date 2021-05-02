using Microsoft.Extensions.DependencyInjection;
using ROS.Common.Mail;
using System;

namespace ROS.Mail
{
	public static class Configuration
	{
		public static IServiceCollection AddMailService(this IServiceCollection services)
		{
			services.AddSingleton(resolver => new Lazy<IMailService>(() =>
			{
				var settings = new MailServerSetting()
				{
					DisplayName = Environment.GetEnvironmentVariable("MAIL_SERVER_DISPLAY_NAME"),
					FromAddress = Environment.GetEnvironmentVariable("MAIL_SERVER_FROM_ADDRESS"),
					Host = Environment.GetEnvironmentVariable("MAIL_SERVER_HOST"),
					SiteUrl = Environment.GetEnvironmentVariable("MAIL_SERVER_SITEURL"),
					UserName = Environment.GetEnvironmentVariable("MAIL_SERVER_USERNAME"),
					Password = Environment.GetEnvironmentVariable("MAIL_SERVER_PASSWORD"),
					Port = int.Parse(Environment.GetEnvironmentVariable("MAIL_SERVER_PORT")),
					UseSSL = bool.Parse(Environment.GetEnvironmentVariable("MAIL_SERVER_USE_SSL")),
				};

				return new MailService(settings);
			}));

			return services;
		}
	}
}
