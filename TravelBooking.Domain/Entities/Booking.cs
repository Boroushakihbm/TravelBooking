namespace TravelBooking.Domain.Entities;

public class Booking
{
    public int Id { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public DateTime BookingDate { get; set; }
    public int SeatCount { get; set; }
}
