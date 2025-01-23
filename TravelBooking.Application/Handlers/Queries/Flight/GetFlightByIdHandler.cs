using MediatR;
using TravelBooking.Common.Queries.Flight;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Queries.Flight;

public class GetFlightByIdHandler : IRequestHandler<GetFlightByIdQuery, Domain.Entities.Flight?>
{
    private readonly IFlightRepository _repository;

    public GetFlightByIdHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Flight?> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id!.Value);
    }
}