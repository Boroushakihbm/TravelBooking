namespace TravelBooking.Domain.Events
{
    public class BookingCreatedEventResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int BookingId { get; set; }
    }
}
