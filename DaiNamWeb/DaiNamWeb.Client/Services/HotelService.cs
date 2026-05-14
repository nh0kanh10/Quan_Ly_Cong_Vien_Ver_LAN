using System.Net.Http.Json;
using DaiNamWeb.Client.Models;

namespace DaiNamWeb.Client.Services;

public class HotelService
{
    private readonly HttpClient _http;
    public HotelService(HttpClient http) => _http = http;

    public async Task<List<RoomTypeInfo>> GetRoomTypes(DateTime checkIn, DateTime checkOut)
        => await _http.GetFromJsonAsync<List<RoomTypeInfo>>(
            $"/api/hotel/room-types?checkIn={checkIn:yyyy-MM-dd}&checkOut={checkOut:yyyy-MM-dd}") ?? new();

    public async Task<BookingResult?> Book(BookingRequest req)
    {
        var resp = await _http.PostAsJsonAsync("/api/hotel/book", req);
        if (resp.IsSuccessStatusCode)
            return await resp.Content.ReadFromJsonAsync<BookingResult>();
        return null;
    }

    public async Task<List<MyBookingItem>> GetMyBookings(int idTaiKhoan)
        => await _http.GetFromJsonAsync<List<MyBookingItem>>($"/api/hotel/my-bookings?idTaiKhoan={idTaiKhoan}") ?? new();
}
