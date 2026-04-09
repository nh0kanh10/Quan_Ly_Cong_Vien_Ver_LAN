using DaiNamWeb.Api.Data;
using DaiNamWeb.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// DI
builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<AuthRepo>();
builder.Services.AddScoped<TicketRepo>();

// CORS — cho Blazor WASM gọi API
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

// ==================== AUTH ====================

app.MapPost("/api/auth/register", async (RegisterRequest req, AuthRepo repo) =>
{
    try
    {
        var result = await repo.Register(req);
        return result is null
            ? Results.BadRequest(new { message = "Số điện thoại đã tồn tại!" })
            : Results.Ok(result);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"REGISTER ERROR: {ex.Message}");
        return Results.BadRequest(new { message = $"Lỗi đăng ký: {ex.Message}" });
    }
});

app.MapPost("/api/auth/login", async (LoginRequest req, AuthRepo repo) =>
{
    var result = await repo.LoginCustomer(req);
    return result is null
        ? Results.Unauthorized()
        : Results.Ok(result);
});

app.MapPost("/api/auth/staff-login", async (LoginRequest req, AuthRepo repo) =>
{
    var result = await repo.LoginStaff(req);
    return result is null
        ? Results.Unauthorized()
        : Results.Ok(result);
});

app.MapGet("/api/auth/profile", async (int idTaiKhoanWeb, AuthRepo repo) =>
{
    var profile = await repo.GetProfile(idTaiKhoanWeb);
    return profile is null ? Results.NotFound() : Results.Ok(profile);
});

app.MapPost("/api/auth/profile/update", async (UpdateProfileRequest req, AuthRepo repo) =>
{
    var success = await repo.UpdateProfile(req);
    return success ? Results.Ok() : Results.BadRequest();
});

// ==================== TICKETS ====================

app.MapGet("/api/tickets/products", async (TicketRepo repo) =>
{
    var products = await repo.GetProducts();
    return Results.Ok(products);
});

app.MapGet("/api/tickets/price", async (int idSanPham, DateTime? date, TicketRepo repo) =>
{
    var price = await repo.GetPrice(idSanPham, date ?? DateTime.Now);
    return Results.Ok(price);
});

app.MapPost("/api/tickets/purchase", async (PurchaseRequest req, TicketRepo repo) =>
{
    try
    {
        var result = await repo.Purchase(req);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = ex.Message });
    }
});

app.MapGet("/api/tickets/my-tickets", async (int idKhachHang, TicketRepo repo) =>
{
    var tickets = await repo.GetMyTickets(idKhachHang);
    return Results.Ok(tickets);
});

app.MapGet("/api/tickets/history", async (int idTaiKhoan, TicketRepo repo) =>
{
    var history = await repo.GetTransactionHistory(idTaiKhoan);
    return Results.Ok(history);
});

// Wallet & Point endpoints
app.MapGet("/api/wallet/points-history", async (int idTaiKhoanWeb, TicketRepo repo) =>
{
    var history = await repo.GetPointHistory(idTaiKhoanWeb);
    return Results.Ok(history);
});

app.MapGet("/api/wallet/transactions", async (int idTaiKhoanWeb, TicketRepo repo) =>
{
    var history = await repo.GetWalletHistory(idTaiKhoanWeb);
    return Results.Ok(history);
});

app.MapGet("/api/wallet/balance", async (int idTaiKhoanWeb, TicketRepo repo) =>
{
    var balance = await repo.GetWalletBalance(idTaiKhoanWeb);
    return Results.Ok(new { SoDu = balance ?? 0m });
});

app.MapPost("/api/tickets/calculate", async (CalculateOrderRequest req, TicketRepo repo) => 
{
    var result = await repo.CalculateOrder(req);
    return Results.Ok(result);
});

// ==================== GATE (SOÁT VÉ) ====================

app.MapGet("/api/gate/khuvuc", async (TicketRepo repo) =>
{
    var list = await repo.GetGateKhuVuc();
    return Results.Ok(list);
});

app.MapGet("/api/gate/thietbi", async (int? idKhuVuc, TicketRepo repo) =>
{
    var list = await repo.GetGateThietBi(idKhuVuc);
    return Results.Ok(list);
});

app.MapPost("/api/gate/scan", async (ScanRequest req, TicketRepo repo) =>
{
    var result = await repo.ScanTicket(req);
    return Results.Ok(result);
});

// ==================== HEALTH CHECK ====================

app.MapGet("/api/health", () => Results.Ok(new { status = "OK", time = DateTime.Now }));

app.Run();
