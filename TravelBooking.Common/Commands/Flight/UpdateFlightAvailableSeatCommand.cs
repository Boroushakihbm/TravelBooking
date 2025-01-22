using MediatR;

namespace TravelBooking.Common.Commands.Flight;

public class UpdateFlightAvailableSeatCommand: IRequest<Domain.Entities.Flight>
{
    public string FlightNumber { get; set; } = string.Empty;
    public int AvailableSeats { get; set; }
}
