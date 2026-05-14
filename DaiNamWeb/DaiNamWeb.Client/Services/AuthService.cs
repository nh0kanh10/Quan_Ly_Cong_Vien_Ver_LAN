using System.Net.Http.Json;
using DaiNamWeb.Client.Models;

namespace DaiNamWeb.Client.Services;

public class AuthService
{
    private readonly HttpClient _http;
    public LoginResponse? CurrentUser { get; private set; }
    public bool IsLoggedIn => CurrentUser != null;
    public event Action? OnChange;

    public AuthService(HttpClient http) => _http = http;

    public async Task<(bool Success, string Message)> Register(string sdt, string pass, string hoTen)
    {
        var resp = await _http.PostAsJsonAsync("/api/auth/register", new RegisterRequest(sdt, pass, hoTen));
        if (resp.IsSuccessStatusCode)
        {
            CurrentUser = await resp.Content.ReadFromJsonAsync<LoginResponse>();
            OnChange?.Invoke();
            return (true, "Đăng ký thành công!");
        }
        var err = await resp.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        return (false, err?["message"] ?? "Đăng ký thất bại!");
    }

    public async Task<(bool Success, string Message)> Login(string sdt, string pass)
    {
        var resp = await _http.PostAsJsonAsync("/api/auth/login", new LoginRequest(sdt, pass));
        if (resp.IsSuccessStatusCode)
        {
            CurrentUser = await resp.Content.ReadFromJsonAsync<LoginResponse>();
            OnChange?.Invoke();
            return (true, "Đăng nhập thành công!");
        }
        return (false, "Sai số điện thoại hoặc mật khẩu!");
    }

    public async Task<CustomerProfile?> GetProfile()
    {
        if (CurrentUser == null) return null;
        return await _http.GetFromJsonAsync<CustomerProfile>($"/api/auth/profile?idTaiKhoan={CurrentUser.IdTaiKhoan}");
    }

    public void Logout()
    {
        CurrentUser = null;
        OnChange?.Invoke();
    }
}
