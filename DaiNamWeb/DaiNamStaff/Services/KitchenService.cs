using System.Net.Http.Json;
using DaiNamStaff.Models;

namespace DaiNamStaff.Services;

public class KitchenService
{
    private readonly HttpClient _http;
    public KitchenService(HttpClient http) => _http = http;

    public async Task<List<KitchenOrderItem>> GetPendingOrders(int? idNhaHang = null)
    {
        try
        {
            var url = "/api/kitchen/pending";
            if (idNhaHang.HasValue) url += $"?idNhaHang={idNhaHang}";
            return await _http.GetFromJsonAsync<List<KitchenOrderItem>>(url) ?? new();
        }
        catch { return new(); }
    }

    public async Task<bool> UpdateStatus(int idLenhBep, string trangThai)
    {
        try
        {
            var res = await _http.PostAsJsonAsync("/api/kitchen/update-status",
                new UpdateStatusRequest(idLenhBep, trangThai));
            return res.IsSuccessStatusCode;
        }
        catch { return false; }
    }

    public async Task<KitchenStats?> GetStats(int? idNhaHang = null)
    {
        try
        {
            var url = "/api/kitchen/stats";
            if (idNhaHang.HasValue) url += $"?idNhaHang={idNhaHang}";
            return await _http.GetFromJsonAsync<KitchenStats>(url);
        }
        catch { return null; }
    }
}
