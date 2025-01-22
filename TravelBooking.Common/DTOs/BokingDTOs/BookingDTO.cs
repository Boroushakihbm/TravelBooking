namespace TravelBooking.Common.DTOs.BokingDTOs;

public class BookingDTO
{
    public int? Id { get; set; }
    public DateTime? BookingDate { get; set; }
    public int? SeatCount { get; set; }

    public string? Origin { get; set; } = string.Empty;
    public string? Destination { get; set; } = string.Empty;
    public DateTime? DepartureTime { get; set; }

    public string? FullName { get; set; } = string.Empty;
}
