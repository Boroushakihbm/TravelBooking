using MediatR;
using TravelBooking.Common.Commands.Booking;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Commands.Booking;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Domain.Entities.Booking>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IFlightRepository _flightRepository;
    private readonly IPassengerRepository _passengerRepository;

    public CreateBookingHandler(IBookingRepository bookingRepository, 
                                IFlightRepository flightRepository, 
                                IPassengerRepository passengerRepository)
    {
        _bookingRepository = bookingRepository;
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
    }

    public async Task<Domain.Entities.Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var flight = await _flightRepository.GetByIdAsync(request.FlightId);
        var passenger = await _passengerRepository.GetByIdAsync(request.PassengerId);

        if (flight == null || passenger == null)
            throw new Exception("Flight or Passenger not found.");

        var Booking = new Domain.Entities.Booking
        {
            BookingDate = DateTime.Now,
            FlightId = request.FlightId,
            SeatCount = request.SeatCount,
            PassengerId = request.PassengerId
        };
        await _bookingRepository.AddAsync(Booking);
        return Booking;
    }
}
