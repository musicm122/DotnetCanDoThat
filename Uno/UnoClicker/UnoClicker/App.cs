using ClickerGame.Interfaces;
using ClickerGame.Services;
using ClickerGame.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace UnoClicker
{
    public class App : Application
    {
        private static Window? _window;
        public static IHost? Host { get; private set; }
       
        protected async override void OnLaunched(LaunchActivatedEventArgs args)
        {          
            var builder = this.CreateBuilder(args)

                // Add navigation support for toolkit controls such as TabBar and NavigationView
                .UseToolkitNavigation()
                .Configure(host => host
#if DEBUG
				// Switch to Development environment when running in DEBUG
				.UseEnvironment(Environments.Development)
#endif
                    .UseLogging(configure: (context, logBuilder) =>
                    {
                        // Configure log levels for different categories of logging
                        logBuilder.SetMinimumLevel(
                            context.HostingEnvironment.IsDevelopment() ?
                                LogLevel.Information :
                                LogLevel.Warning);
                    }, enableUnoLogging: true)
                    .UseConfiguration(configure: configBuilder =>
                        configBuilder
                            .EmbeddedSource<App>()
                            .Section<AppConfig>()
                    )
                    // Enable localization (see appsettings.json for supported languages)
                    .UseLocalization()                    
                    .ConfigureServices((context, services) =>
                    {
                        services.AddSingleton<IInventory, Inventory>();
                        services.AddTransient<CookieCounterViewModel>();
                        services.AddTransient<HotDogCounterViewModel>();
                        services.AddTransient<IGameViewModel, GameViewModel>();
                    })
                    .UseNavigation(RegisterRoutes)
                );
            _window = builder.Window;

            Host = await builder.NavigateAsync<Shell>();
        }

        private static void RegisterRoutes(IViewRegistry views, IRouteRegistry routes)
        {
            
            views.Register(
                new ViewMap(ViewModel: typeof(ShellViewModel)),
                new ViewMap<MainPage, GameViewModel>()
            );

            routes.Register(
                new RouteMap("", View: views.FindByViewModel<ShellViewModel>(),
                    Nested: new RouteMap[] {
                        new RouteMap("Main", View: views.FindByViewModel<GameViewModel>()),
                    }
                )
            );
        }        
    }
}