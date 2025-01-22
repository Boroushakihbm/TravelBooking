using MediatR;
using TravelBooking.Common.Commands.Booking;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Commands.Booking;

public class CreateBookingHandler : IRequestHandler<CreateBookingCommand, Domain.Entities.Booking>
{
    private readonly IBookingRepository _repository;

    public CreateBookingHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Booking> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
    {
        var Booking = new Domain.Entities.Booking
        {
            BookingDate = request.BookingDate,
            FlightId = request.FlightId,
            SeatNumber = request.SeatNumber,
            PassengerId = request.PassengerId
        };
        await _repository.AddAsync(Booking);
        return Booking;
    }
}
