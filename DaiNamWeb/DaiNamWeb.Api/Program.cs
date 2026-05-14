using DaiNamWeb.Api.Data;
using DaiNamWeb.Api.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DbContext>();
builder.Services.AddScoped<AuthRepo>();
builder.Services.AddScoped<TicketRepo>();
builder.Services.AddScoped<HotelRepo>();
builder.Services.AddScoped<GateRepo>();
builder.Services.AddScoped<KitchenRepo>();

// CORS: chỉ cho phép domain cụ thể (không AllowAnyOrigin để tránh CSRF từ attacker.com)
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
    ?? new[] { "http://localhost:5177", "https://dainam.vercel.app" };

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();
app.UseCors();

// Middleware xác thực token đơn giản cho các route yêu cầu đăng nhập.
// Client gửi header: Authorization: Bearer <token>
// Token format: "idTaiKhoan:role:expiry" encode Base64
static (bool ok, int idTaiKhoan) ValidateToken(HttpContext ctx)
{
    var header = ctx.Request.Headers["Authorization"].FirstOrDefault();
    if (header == null || !header.StartsWith("Bearer ")) return (false, 0);
    try
    {
        var payload = Encoding.UTF8.GetString(Convert.FromBase64String(header[7..]));
        var parts = payload.Split(':');
        var expiry = DateTime.Parse(parts[2]);
        if (expiry < DateTime.UtcNow) return (false, 0);
        return (true, int.Parse(parts[0]));
    }
    catch { return (false, 0); }
}

//  AUTH 

app.MapPost("/api/auth/register", async (RegisterRequest req, AuthRepo repo) =>
{
    try
    {
        var result = await repo.Register(req);
        return result is null
            ? Results.BadRequest(new { message = "Số điện thoại đã được đăng ký!" })
            : Results.Ok(result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { message = $"Lỗi đăng ký: {ex.Message}" });
    }
});

app.MapPost("/api/auth/login", async (LoginRequest req, AuthRepo repo) =>
{
    var result = await repo.Login(req);
    return result is null ? Results.Unauthorized() : Results.Ok(result);
});

// Profile chỉ xem được của chính mình — idTaiKhoan lấy từ token, không nhận từ query
app.MapGet("/api/auth/profile", async (HttpContext ctx, AuthRepo repo) =>
{
    var (ok, idTaiKhoan) = ValidateToken(ctx);
    if (!ok) return Results.Unauthorized();

    var profile = await repo.GetProfile(idTaiKhoan);
    return profile is null ? Results.NotFound() : Results.Ok(profile);
});

//  VÉ 

app.MapGet("/api/tickets/products", async (TicketRepo repo) =>
{
    try { return Results.Ok(await repo.GetProducts()); }
    catch (Exception ex) { return Results.BadRequest(new { error = ex.Message }); }
});

app.MapGet("/api/tickets/price", async (int idSanPham, DateTime? date, TicketRepo repo) =>
    Results.Ok(await repo.GetPrice(idSanPham, date ?? DateTime.Now)));

app.MapPost("/api/tickets/purchase", async (HttpContext ctx, PurchaseRequest req, TicketRepo repo) =>
{
    // Token phải khớp idTaiKhoan trong request body
    var (ok, idFromToken) = ValidateToken(ctx);
    if (!ok || idFromToken != req.IdTaiKhoan) return Results.Unauthorized();

    try { return Results.Ok(await repo.Purchase(req)); }
    catch (Exception ex) { return Results.BadRequest(new { message = ex.Message }); }
});

// idTaiKhoan lấy từ token, không cần truyền qua query string
app.MapGet("/api/tickets/my-tickets", async (HttpContext ctx, TicketRepo repo) =>
{
    var (ok, idTaiKhoan) = ValidateToken(ctx);
    if (!ok) return Results.Unauthorized();
    return Results.Ok(await repo.GetMyTickets(idTaiKhoan));
});

//  KHÁCH SẠN 

app.MapGet("/api/hotel/room-types", async (DateTime checkIn, DateTime checkOut, HotelRepo repo) =>
    Results.Ok(await repo.GetRoomTypes(checkIn, checkOut)));

app.MapPost("/api/hotel/book", async (HttpContext ctx, BookingRequest req, HotelRepo repo) =>
{
    var (ok, idFromToken) = ValidateToken(ctx);
    if (!ok || idFromToken != req.IdTaiKhoan) return Results.Unauthorized();

    try { return Results.Ok(await repo.Book(req)); }
    catch (Exception ex) { return Results.BadRequest(new { message = ex.Message }); }
});

app.MapGet("/api/hotel/my-bookings", async (HttpContext ctx, HotelRepo repo) =>
{
    var (ok, idTaiKhoan) = ValidateToken(ctx);
    if (!ok) return Results.Unauthorized();
    return Results.Ok(await repo.GetMyBookings(idTaiKhoan));
});

// Hủy đặt phòng — hoàn cọc 100% nếu hủy trước 3 ngày, không hoàn nếu trễ hơn
app.MapPost("/api/hotel/cancel", async (HttpContext ctx, CancelBookingRequest req, HotelRepo repo) =>
{
    var (ok, idFromToken) = ValidateToken(ctx);
    if (!ok || idFromToken != req.IdTaiKhoan) return Results.Unauthorized();

    var result = await repo.CancelBooking(req);
    return result.ThanhCong ? Results.Ok(result) : Results.BadRequest(result);
});

//  SOÁT VÉ CỔNG (nhân viên) 

app.MapGet("/api/gate/areas", async (GateRepo repo) => Results.Ok(await repo.GetAreas()));

app.MapGet("/api/gate/games", async (int? idKhuVuc, GateRepo repo) =>
    Results.Ok(await repo.GetGames(idKhuVuc)));

app.MapGet("/api/gate/history", async (int? idKhuVuc, int? limit, GateRepo repo) =>
    Results.Ok(await repo.GetRecentScans(idKhuVuc, limit ?? 20)));

// POST thay vì GET: mã vé nằm trong body, không lộ trong URL/server log/CDN cache
app.MapPost("/api/gate/scan", async (ScanRequest req, GateRepo repo) =>
    Results.Ok(await repo.ScanTicket(req.MaVach, req.IdKhuVuc, req.IdTroChoi)));

//  BẾP KDS (nhân viên) 

app.MapGet("/api/kitchen/pending", async (int? idNhaHang, KitchenRepo repo) =>
    Results.Ok(await repo.GetPendingOrders(idNhaHang)));

app.MapPost("/api/kitchen/update-status", async (UpdateKitchenStatusRequest req, KitchenRepo repo) =>
{
    var ok = await repo.UpdateStatus(req.IdLenhBep, req.TrangThai);
    return ok ? Results.Ok() : Results.BadRequest(new { message = "Cập nhật thất bại" });
});

app.MapGet("/api/kitchen/stats", async (int? idNhaHang, KitchenRepo repo) =>
    Results.Ok(await repo.GetStats(idNhaHang)));

//  HEALTH 

app.MapGet("/api/health", () => Results.Ok(new { status = "OK", time = DateTime.Now }));

app.Run();
