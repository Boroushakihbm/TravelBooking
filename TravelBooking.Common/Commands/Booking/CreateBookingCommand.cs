namespace TravelBooking.Common.Commands.Booking;

public class CreateBookingCommand : IRequest<Domain.Entities.Booking>
{
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public DateTime BookingDate { get; set; }
    public int SeatCount { get; set; }
}
