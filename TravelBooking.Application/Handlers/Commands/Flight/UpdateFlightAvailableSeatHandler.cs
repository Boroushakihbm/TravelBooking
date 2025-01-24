using MediatR;
using TravelBooking.Common.Commands.Flight;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Commands.Flight;

public class UpdateFlightAvailableSeatHandler : IRequestHandler<UpdateFlightAvailableSeatCommand, Domain.Entities.Flight>
{
    private readonly IFlightRepository _repository;

    public UpdateFlightAvailableSeatHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Flight> Handle(UpdateFlightAvailableSeatCommand request, CancellationToken cancellationToken)
    {
        var flight = await _repository.GetByFlightNumberAsync(request.FlightNumber);

        if (flight == null)
            throw new KeyNotFoundException("Flight NotFound");

        flight.AvailableSeats = request.AvailableSeats;
        await _repository.UpdateAsync(flight);
        return flight;
    }
}