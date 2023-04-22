using BlazorComponents.ViewModel;
using BlazorClickerApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

void RegisterVM(IServiceCollection serviceCollection)
{
    var gameVm = new GameViewModel();
    serviceCollection.AddSingleton(gameVm.CookieCounter);
    serviceCollection.AddSingleton(gameVm);
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