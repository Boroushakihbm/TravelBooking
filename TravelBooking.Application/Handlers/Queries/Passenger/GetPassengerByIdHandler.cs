using MediatR;
using TravelBooking.Common.Queries.Passenger;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Queries.Passenger;

public class GetPassengerByIdHandler : IRequestHandler<GetPassengerByIdQuery, Domain.Entities.Passenger?>
{
    private readonly IPassengerRepository _repository;

    public GetPassengerByIdHandler(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Passenger?> Handle(GetPassengerByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id!.Value);
    }
}