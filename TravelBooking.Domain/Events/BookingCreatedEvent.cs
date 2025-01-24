namespace TravelBooking.Domain.Events
{
    public class BookingCreatedEvent
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public DateTime BookingDate { get; set; }
        public int SeatCount { get; set; }

        public BookingCreatedEvent(int id, int flightId, int passengerId, DateTime bookingDate, int seatCount)
        {
            Id = id;
            FlightId = flightId;
            PassengerId = passengerId;
            BookingDate = bookingDate;
            SeatCount = seatCount;
        }
    }
}
