using BlazorComponents.ViewModel;
using BlazorClickerApp;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});
builder.Services.AddSingleton<GameViewModel>();
builder.Services.AddSingleton<CookieCounterViewModel>();

builder.Services.AddMvvm();


await builder.Build().RunAsync();