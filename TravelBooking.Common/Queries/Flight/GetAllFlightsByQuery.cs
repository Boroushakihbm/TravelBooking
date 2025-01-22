using MediatR;

namespace TravelBooking.Common.Queries.Flight;

public class GetAllFlightsByQuery : IRequest<List<Domain.Entities.Flight>?>
{
    public string? FlightNumber { get; set; }
    public string? Origin { get; set; }
    public string? Destination { get; set; }
    public DateTime? DepartureTime { get; set; }
    public DateTime? ArrivalTime { get; set; }
    public int? AvailableSeats { get; set; }
    public decimal? Price { get; set; }
    public int take { get; set; } = 10;
    public int skip { get; set; } = 0;
}
