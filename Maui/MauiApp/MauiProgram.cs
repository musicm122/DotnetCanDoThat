using Microsoft.Extensions.Logging;
using ClickerGame.Services;
using ClickerGame.ViewModels;

namespace MauiAppClickerGame;

public static class MauiProgram
{
	public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
	{
		mauiAppBuilder.Services.AddSingleton<GameViewModel>();
		return mauiAppBuilder; 
	}

	public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
	{
		var builder = 
			Microsoft.Maui.Hosting.MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.RegisterViewModels()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();

		builder.Logging.AddDebug();
#endif
		builder.Services.AddSingleton<Game>();

		//builder.Services.AddSingleton<WeatherForecastService>();

		return builder.Build();
	}
}
