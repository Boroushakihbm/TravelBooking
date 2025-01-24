using MassTransit;
using TravelBooking.Domain.Events;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Consumers
{
    public class BookingCreatedEventConsumer : IConsumer<BookingCreatedEvent>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;
        public BookingCreatedEventConsumer(IBookingRepository bookingRepository,
                                            IFlightRepository flightRepository,
                                            IPassengerRepository passengerRepository)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
        }

        public async Task Consume(ConsumeContext<BookingCreatedEvent> context)
        {
            var @event = context.Message;

            var flight = await _flightRepository.GetByIdAsync(@event.FlightId);
            var passenger = await _passengerRepository.GetByIdAsync(@event.PassengerId);

            if (flight == null || passenger == null)
                throw new KeyNotFoundException("Flight or Passenger not found.");
            if (flight.AvailableSeats == 0 || (flight.AvailableSeats - @event.SeatCount) < 0)
                throw new KeyNotFoundException("Flight Not Available Seat.");

            var booking = new Domain.Entities.Booking()
            {
                Id = @event.Id,
                FlightId = @event.FlightId,
                BookingDate = @event.BookingDate,
                PassengerId = @event.PassengerId,
                SeatCount = @event.SeatCount
            };

            flight.AvailableSeats = flight.AvailableSeats - @event.SeatCount;

            await _flightRepository.UpdateAsync(flight);
            await _bookingRepository.AddAsync(booking);

            await context.RespondAsync(new BookingCreatedEventResponse
            {
                Success = true,
                Message = "Booking created successfully",
                BookingId = booking.Id
            });
        }
    }

}
