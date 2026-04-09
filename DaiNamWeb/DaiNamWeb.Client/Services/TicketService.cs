using System.Net.Http.Json;
using DaiNamWeb.Client.Models;

namespace DaiNamWeb.Client.Services;

public class TicketService
{
    private readonly HttpClient _http;
    public TicketService(HttpClient http) => _http = http;

    public async Task<List<TicketProduct>> GetProducts()
    {
        return await _http.GetFromJsonAsync<List<TicketProduct>>("/api/tickets/products") ?? new();
    }

    public async Task<TicketPrice?> GetPrice(int idSanPham)
    {
        return await _http.GetFromJsonAsync<TicketPrice>(
            $"/api/tickets/price?idSanPham={idSanPham}&date={DateTime.Now:yyyy-MM-dd}");
    }

    public async Task<PurchaseResult?> Purchase(PurchaseRequest req)
    {
        var resp = await _http.PostAsJsonAsync("/api/tickets/purchase", req);
        if (resp.IsSuccessStatusCode)
            return await resp.Content.ReadFromJsonAsync<PurchaseResult>();
        return null;
    }

    public async Task<CalculateOrderResult?> CalculateOrder(List<CartItem> items, int idTaiKhoanWeb, bool usePoints)
    {
        var resp = await _http.PostAsJsonAsync("/api/tickets/calculate",
            new CalculateOrderRequest(items, idTaiKhoanWeb, usePoints));
        if (resp.IsSuccessStatusCode)
            return await resp.Content.ReadFromJsonAsync<CalculateOrderResult>();
        return null;
    }

    public async Task<List<TicketInfo>> GetMyTickets(int idKhachHang)
    {
        return await _http.GetFromJsonAsync<List<TicketInfo>>(
            $"/api/tickets/my-tickets?idKhachHang={idKhachHang}") ?? new();
    }

    public async Task<IEnumerable<TransactionHistoryItem>> GetPurchaseHistory(int idTaiKhoanWeb)
    {
        return await _http.GetFromJsonAsync<IEnumerable<TransactionHistoryItem>>($"/api/tickets/history?idTaiKhoan={idTaiKhoanWeb}") 
               ?? Enumerable.Empty<TransactionHistoryItem>();
    }

    public async Task<IEnumerable<PointHistoryItem>> GetPointHistory(int idTaiKhoanWeb)
    {
        return await _http.GetFromJsonAsync<IEnumerable<PointHistoryItem>>($"/api/wallet/points-history?idTaiKhoanWeb={idTaiKhoanWeb}") 
               ?? Enumerable.Empty<PointHistoryItem>();
    }

    public async Task<IEnumerable<WalletTransactionItem>> GetWalletHistory(int idTaiKhoanWeb)
    {
        return await _http.GetFromJsonAsync<IEnumerable<WalletTransactionItem>>($"/api/wallet/transactions?idTaiKhoanWeb={idTaiKhoanWeb}") 
               ?? Enumerable.Empty<WalletTransactionItem>();
    }

    public class WalletBalanceResult { public decimal SoDu { get; set; } }

    public async Task<decimal?> GetWalletBalance(int idTaiKhoanWeb)
    {
        var res = await _http.GetFromJsonAsync<WalletBalanceResult>($"/api/wallet/balance?idTaiKhoanWeb={idTaiKhoanWeb}");
        return res?.SoDu; // Notice: the api returns { SoDu = balance ?? 0m }, so if null it means no wallet or 0, our UI will handle 0.
    }

    public async Task<ScanResult?> ScanTicket(ScanRequest req)
    {
        var resp = await _http.PostAsJsonAsync("/api/gate/scan", req);
        return await resp.Content.ReadFromJsonAsync<ScanResult>();
    }

    public async Task<List<GateKhuVuc>> GetGateKhuVuc()
    {
        return await _http.GetFromJsonAsync<List<GateKhuVuc>>("/api/gate/khuvuc") ?? new();
    }

    public async Task<List<GateThietBi>> GetGateThietBi(int? idKhuVuc)
    {
        var url = idKhuVuc.HasValue ? $"/api/gate/thietbi?idKhuVuc={idKhuVuc}" : "/api/gate/thietbi";
        return await _http.GetFromJsonAsync<List<GateThietBi>>(url) ?? new();
    }
}
