namespace TravelBooking.Common.Queries.Booking;
public class GetBookingByIdQuery : IRequest<Domain.Entities.Booking>
{
    public Guid? Id { get; set; }
}
