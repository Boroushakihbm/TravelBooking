namespace TravelBooking.Common.Commands.Booking;

public class UpdateBookingCommand
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public int SeatCount { get; set; }
}
