namespace TravelBooking.Common.Queries.Passenger;
public class GetPassengerByIdQuery : IRequest<Domain.Entities.Passenger>
{
    public int? Id { get; set; }
}
