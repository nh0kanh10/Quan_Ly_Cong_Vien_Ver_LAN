using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DaiNamStaff;
using DaiNamStaff.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBase = builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:5245";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });
builder.Services.AddScoped<StaffAuthService>();
builder.Services.AddScoped<GateService>();
builder.Services.AddScoped<KitchenService>();

await builder.Build().RunAsync();
