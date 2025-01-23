using MediatR;
using TravelBooking.Common.Commands.Booking;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Repositories;

namespace TravelBooking.Application.Handlers.Commands.Booking;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Domain.Entities.Booking>
{
    private readonly IBookingRepository _repository;

    private readonly IFlightRepository _flightRepository;
    private readonly IPassengerRepository _passengerRepository;

    public CreateBookingHandler(IFlightRepository flightRepository, 
                                IPassengerRepository passengerRepository)
    {
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
    }

    public async Task<Domain.Entities.Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var flight = _flightRepository.GetByIdAsync(request.FlightId);
        var passenger = _passengerRepository.GetByIdAsync(request.PassengerId);

        if (flight == null || passenger == null)
            throw new Exception("Flight or Passenger not found.");

        var Booking = new Domain.Entities.Booking
        {
            BookingDate = request.BookingDate,
            FlightId = request.FlightId,
            SeatCount = request.SeatCount,
            PassengerId = request.PassengerId
        };
        await _repository.AddAsync(Booking);
        return Booking;
    }
}
