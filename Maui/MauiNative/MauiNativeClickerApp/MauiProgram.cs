using ClickerGame.Interfaces;
using ClickerGame.Services;
using ClickerGame.ViewModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace MauiNativeClickerApp
{
	public static class MauiProgram
	{
		public static MauiAppBuilder RegisterDependencies(this MauiAppBuilder mauiAppBuilder) 
		{
            mauiAppBuilder.Services.AddSingleton<IInventory, Inventory>();
            mauiAppBuilder.Services.AddTransient<CookieCounterViewModel>();
            mauiAppBuilder.Services.AddTransient<HotDogCounterViewModel>();
            mauiAppBuilder.Services.AddTransient<GameViewModel>();
            return mauiAppBuilder;
		}

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder) 
		{
			mauiAppBuilder.Services.AddSingleton<MainPage>();
			return mauiAppBuilder;
        }


        public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
                .RegisterDependencies()
				.RegisterViews()
                .UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
		builder.Logging.AddDebug();
#endif
            return builder.Build();
		}

		
	}
}