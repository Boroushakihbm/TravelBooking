using MassTransit;
using MediatR;
using TravelBooking.Common.Commands.Booking;
using TravelBooking.Domain.Events;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Commands.Booking;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Domain.Entities.Booking>
{
    private readonly IFlightRepository _flightRepository;
    private readonly IPassengerRepository _passengerRepository;
    private readonly IBus _bus;
    private readonly IRequestClient<BookingCreatedEvent> _client;

    public CreateBookingHandler(IFlightRepository flightRepository, 
                                IPassengerRepository passengerRepository,
                                IBus bus,
                                IRequestClient<BookingCreatedEvent> client)
    {
        _flightRepository = flightRepository;
        _passengerRepository = passengerRepository;
        _bus = bus;
        _client = client;
    }
   
    public async Task<Domain.Entities.Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {

        var flight = await _flightRepository.GetByIdAsync(request.FlightId);
        var passenger = await _passengerRepository.GetByIdAsync(request.PassengerId);

        if (flight == null || passenger == null)
            throw new Exception("Flight or Passenger not found.");
        if (flight.AvailableSeats == 0 || (flight.AvailableSeats - request.SeatCount) < 0)
            throw new Exception("Flight Not Available Seat.");

        var bookingCreated = Domain.Entities.Booking.Create(request.FlightId, request.PassengerId, DateTime.Now, request.SeatCount);
        var response = await _client.GetResponse<CreateBookingResponse>(bookingCreated.Item2);

        return bookingCreated.Item1;
    }
}
