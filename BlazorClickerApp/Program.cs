using BlazorComponents.ViewModel;
using BlazorClickerApp;
using ClickerGame.Models;
using ClickerGame.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

void RegisterVM(IServiceCollection serviceCollection)
{
    var inventory = new Inventory();
    var gameVm = new GameViewModel(inventory);
    serviceCollection.AddSingleton(inventory);
    serviceCollection.AddSingleton(gameVm.CookieCounter);
    //todo: update registration for hotdog
    //serviceCollection.AddSingleton(gameVm.CookieCounter);
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