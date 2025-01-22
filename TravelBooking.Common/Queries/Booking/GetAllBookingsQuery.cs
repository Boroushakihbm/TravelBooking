using TravelBooking.Common.DTOs.BokingDTOs;

namespace TravelBooking.Common.Queries.Booking;

public class GetAllBookingsQuery: IRequest<IEnumerable<BookingDTO>?>
{
    public string? FlightNumber { get; set; }
    public int take { get; set; } = 10;
    public int skip { get; set; } = 0;
}
