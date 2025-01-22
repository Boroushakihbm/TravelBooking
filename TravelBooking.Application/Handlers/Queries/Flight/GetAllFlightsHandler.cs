using MediatR;
using Microsoft.IdentityModel.Tokens;
using TravelBooking.Common.Queries.Flight;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Queries.Flight;

public class GetAllFlightsHandler : IRequestHandler<GetAllFlightsByQuery, List<Domain.Entities.Flight>?>
{
    private readonly IFlightRepository _repository;

    public GetAllFlightsHandler(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Domain.Entities.Flight>?> Handle(GetAllFlightsByQuery filterFlight, CancellationToken cancellationToken)
    {
        return await _repository
                   .GetByFilterAsync(flight =>
                    (!filterFlight.ArrivalTime.HasValue || flight.ArrivalTime == filterFlight.ArrivalTime) &&
                    (!filterFlight.AvailableSeats.HasValue || flight.AvailableSeats == filterFlight.AvailableSeats) &&
                    (!filterFlight.DepartureTime.HasValue || flight.DepartureTime == filterFlight.DepartureTime) &&
                    (!filterFlight.Price.HasValue || flight.Price == filterFlight.Price) &&
                    (filterFlight.Destination.IsNullOrEmpty() || flight.Destination == filterFlight.Destination) &&
                    (filterFlight.FlightNumber.IsNullOrEmpty() || flight.FlightNumber == filterFlight.FlightNumber) &&
                    (filterFlight.Origin.IsNullOrEmpty() || flight.Origin == filterFlight.Origin)
                    ,filterFlight.take
                    ,filterFlight.skip);
    }
}