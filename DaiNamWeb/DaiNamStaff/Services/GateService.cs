using System.Net.Http.Json;
using DaiNamStaff.Models;

namespace DaiNamStaff.Services;

public class GateService
{
    private readonly HttpClient _http;
    public GateService(HttpClient http) => _http = http;

    public async Task<List<AreaItem>> GetAreas()
    {
        try { return await _http.GetFromJsonAsync<List<AreaItem>>("/api/gate/areas") ?? new(); }
        catch { return new(); }
    }

    public async Task<List<GameItem>> GetGames(int? idKhuVuc)
    {
        try { return await _http.GetFromJsonAsync<List<GameItem>>($"/api/gate/games?idKhuVuc={idKhuVuc}") ?? new(); }
        catch { return new(); }
    }

    public async Task<List<ScanHistoryItem>> GetHistory(int? idKhuVuc)
    {
        try { return await _http.GetFromJsonAsync<List<ScanHistoryItem>>($"/api/gate/history?idKhuVuc={idKhuVuc}") ?? new(); }
        catch { return new(); }
    }

    public async Task<ScanResult?> Scan(string maVach, int? idKhuVuc = null, int? idTroChoi = null)
    {
        try
        {
            string url = $"/api/gate/scan?maVach={maVach}";
            if (idKhuVuc.HasValue) url += $"&idKhuVuc={idKhuVuc}";
            if (idTroChoi.HasValue) url += $"&idTroChoi={idTroChoi}";
            return await _http.GetFromJsonAsync<ScanResult>(url);
        }
        catch { return null; }
    }
}
