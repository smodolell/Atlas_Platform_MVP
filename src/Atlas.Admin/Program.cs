using Atlas.Admin;
using Atlas.Client.Extensions;
using Atlas.Components.Extensions;
using BitzArt.Blazor.Cookies;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

const int ApplicationId = 1;
const string ApplicationName = "Altas Admin";

builder.Services.AddYggdrasilApplication(options =>
{
    options.ApplicationId = ApplicationId;
    options.ApplicationName = ApplicationName;
});


var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "";

builder.AddBlazorCookies();
builder.Services.AddAtlasClient(apiBaseUrl);
builder.Services.AddAtlasComponents();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


var host = builder.Build();

await host.Services.RunInitializersAsync();

await host.RunAsync();
