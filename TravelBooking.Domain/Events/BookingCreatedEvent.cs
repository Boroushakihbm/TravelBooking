namespace TravelBooking.Domain.Events
{
    public class BookingCreatedEvent
    {
        public Guid Id { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public DateTime BookingDate { get; set; }
        public int SeatCount { get; set; }

        public BookingCreatedEvent(Guid id, int flightId, int passengerId, DateTime bookingDate, int seatCount)
        {
            Id = id;
            FlightId = flightId;
            PassengerId = passengerId;
            BookingDate = bookingDate;
            SeatCount = seatCount;
        }
    }

    public class CreateBookingResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Guid BookingId { get; set; }
    }
}
