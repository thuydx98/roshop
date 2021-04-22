using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace ROS.Contracts.Configures
{
	public static class Localization
	{
		public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
		{
			app.UseRequestLocalization(new RequestLocalizationOptions()
			{
				DefaultRequestCulture = new RequestCulture("vi"),
				SupportedCultures = new[] { new CultureInfo("en"), new CultureInfo("vi") },
				SupportedUICultures = new[] { new CultureInfo("en"), new CultureInfo("vi") },
				RequestCultureProviders = new IRequestCultureProvider[] {
					new QueryStringRequestCultureProvider(),
					new AcceptLanguageHeaderRequestCultureProvider()
				},
			});

			return app;
		}
	}
}
