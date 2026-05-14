using System.Net.Http.Json;
using DaiNamStaff.Models;

namespace DaiNamStaff.Services;

public class StaffAuthService
{
    private readonly HttpClient _http;
    public LoginResponse? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;

    public StaffAuthService(HttpClient http) => _http = http;

    public async Task<(bool ok, string msg)> Login(string sdt, string pass)
    {
        try
        {
            var res = await _http.PostAsJsonAsync("/api/auth/login", new LoginRequest(sdt, pass));
            if (!res.IsSuccessStatusCode)
                return (false, "Sai số điện thoại hoặc mật khẩu");

            CurrentUser = await res.Content.ReadFromJsonAsync<LoginResponse>();
            return (true, $"Xin chào {CurrentUser?.HoTen}");
        }
        catch { return (false, "Không kết nối được server"); }
    }

    public void Logout() => CurrentUser = null;
}
