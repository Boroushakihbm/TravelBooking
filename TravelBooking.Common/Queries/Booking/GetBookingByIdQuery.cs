namespace TravelBooking.Common.Queries.Booking;
public class GetBookingByIdQuery : IRequest<Domain.Entities.Booking>
{
    public int? Id { get; set; }
}
