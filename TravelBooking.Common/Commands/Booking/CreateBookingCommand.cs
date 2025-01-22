using MediatR;
using TravelBooking.Domain.Entities;

namespace TravelBooking.Common.Commands.Booking;

public class CreateBookingCommand : IRequest<Domain.Entities.Booking>
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public DateTime BookingDate { get; set; }
    public string SeatNumber { get; set; } = string.Empty;
}
