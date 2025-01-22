using MediatR;
using TravelBooking.Common.Commands.Flight;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Commands.Flight;

public class CreateFlightHandler : IRequestHandler<CreateFlightCommand, Domain.Entities.Flight>
{
    private readonly IFlightRepository _repository;

    public CreateFlightHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Flight> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = new Domain.Entities.Flight
        {
            FlightNumber = request.FlightNumber,
            Origin = request.Origin,
            Destination = request.Destination,
            DepartureTime = request.DepartureTime,
            ArrivalTime = request.ArrivalTime,
            AvailableSeats = request.AvailableSeats,
            Price = request.Price
        };
        await _repository.AddAsync(flight);
        return flight;
    }
}
