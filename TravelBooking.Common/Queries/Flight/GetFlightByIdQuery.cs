using MediatR;
namespace TravelBooking.Common.Queries.Flight;
public class GetFlightByIdQuery : IRequest<Domain.Entities.Flight>
{
    public int Id { get; set; }
}