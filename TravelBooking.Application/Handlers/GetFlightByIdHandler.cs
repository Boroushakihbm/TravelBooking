using MediatR;
using TravelBooking.Common.Queries.Flight;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers;

public class GetFlightByIdHandler : IRequestHandler<GetFlightByIdQuery, Flight?>
{
    private readonly IFlightRepository _repository;

    public GetFlightByIdHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<Flight?> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}