using TravelBooking.Domain.Events;

namespace TravelBooking.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }
    public int FlightId { get; set; }
    public int PassengerId { get; set; }
    public DateTime BookingDate { get; set; }
    public int SeatCount { get; set; }

    public Booking() { }

    public Booking(Guid id, int flightId, int passengerId, DateTime bookingDate, int seatCount)
    {
        Id = id;
        FlightId = flightId;
        PassengerId = passengerId;
        BookingDate = bookingDate;
        SeatCount = seatCount;
    }
    public static Tuple<Booking, BookingCreatedEvent> Create(int flightId, int passengerId, DateTime bookingDate, int seatCount)
    {
        var id = Guid.NewGuid();
        var booking = new Booking(id, flightId, passengerId, bookingDate, seatCount);
        var @event = new BookingCreatedEvent(id, flightId, passengerId, bookingDate, seatCount);

        return new Tuple<Booking, BookingCreatedEvent>(booking, @event);
    }
}
