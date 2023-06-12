using BlazorComponents.ViewModel;
using BlazorClickerApp;
using ClickerGame.Models;
using ClickerGame.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BazorComponents;
using BlazorMvvm.Common.ViewModel;

static void RegisterVM(IServiceCollection serviceCollection)
{
    serviceCollection.AddSingleton<Inventory>();
    serviceCollection.AddSingleton<CookieCounterBlazorViewModel>();
    serviceCollection.AddSingleton<HotDogBlazorViewModel>();
    serviceCollection.AddSingleton<GameViewModel>();    
    
}

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

RegisterVM(builder.Services);
builder.Services.AddMvvm();


await builder.Build().RunAsync();