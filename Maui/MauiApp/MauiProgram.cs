using Microsoft.Extensions.Logging;
using MauiAppClickerGame.Data;

namespace MauiAppClickerGame;

public static class MauiProgram
{
	public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
	{
		var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();

		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<ClickerGame.Game>();
		builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
