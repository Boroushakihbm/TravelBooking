using MediatR;
using TravelBooking.Application.Commands;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers;

public class CreateFlightHandler : IRequestHandler<CreateFlightCommand, Flight>
{
    private readonly IFlightRepository _repository;

    public CreateFlightHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<Flight> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = new Flight
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
