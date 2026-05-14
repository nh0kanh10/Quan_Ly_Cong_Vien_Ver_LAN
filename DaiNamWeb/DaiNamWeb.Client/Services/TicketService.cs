using System.Net.Http.Json;
using DaiNamWeb.Client.Models;

namespace DaiNamWeb.Client.Services;

public class TicketService
{
    private readonly HttpClient _http;
    public TicketService(HttpClient http) => _http = http;

    public async Task<List<TicketProduct>> GetProducts()
        => await _http.GetFromJsonAsync<List<TicketProduct>>("/api/tickets/products") ?? new();

    public async Task<TicketPrice?> GetPrice(int idSanPham)
        => await _http.GetFromJsonAsync<TicketPrice>($"/api/tickets/price?idSanPham={idSanPham}&date={DateTime.Now:yyyy-MM-dd}");

    public async Task<PurchaseResult?> Purchase(PurchaseRequest req)
    {
        var resp = await _http.PostAsJsonAsync("/api/tickets/purchase", req);
        if (resp.IsSuccessStatusCode)
            return await resp.Content.ReadFromJsonAsync<PurchaseResult>();
        return null;
    }

    public async Task<List<TicketInfo>> GetMyTickets(int idTaiKhoan)
        => await _http.GetFromJsonAsync<List<TicketInfo>>($"/api/tickets/my-tickets?idTaiKhoan={idTaiKhoan}") ?? new();
}
