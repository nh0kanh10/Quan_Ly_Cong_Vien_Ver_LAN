using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DaiNamWeb.Client;
using DaiNamWeb.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// API base URL — đổi khi deploy qua Cloudflare Tunnel / Ngrok
var apiBase = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5245";
builder.Services.AddScoped(sp => 
{
    var client = new HttpClient { BaseAddress = new Uri(apiBase) };
    client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "true");
    return client;
});
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<TicketService>();

await builder.Build().RunAsync();
