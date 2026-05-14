using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DaiNamWeb.Client;
using DaiNamWeb.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Đọc base URL API từ appsettings.json
var apiBase = builder.Configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:5245";

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(apiBase) });
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TicketService>();
builder.Services.AddScoped<HotelService>();

await builder.Build().RunAsync();
