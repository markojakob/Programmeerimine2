using BlazorApp;
using KooliProjekt.PublicApi;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7136") });
builder.Services.AddScoped<IApiClient, ApiClient>();
builder.Services.AddScoped<IRentingApiClient, RentingApiClient>();
await builder.Build().RunAsync();
