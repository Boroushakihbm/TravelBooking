using MassTransit;
using Moq;
using TravelBooking.Application.Handlers.Commands.Booking;
using TravelBooking.Common.Commands.Booking;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Events;
using TravelBooking.Domain.Interfaces;
using Xunit;

namespace TravelBooking.Application.Tests.Handlers.Commands.BookingTests
{
    public class CreateBookingHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly Mock<IPassengerRepository> _passengerRepositoryMock;
        private readonly Mock<IRequestClient<BookingCreatedEvent>> _clientMock;
        private readonly CreateBookingHandler _handler;

        public CreateBookingHandlerTests()
        {
            _flightRepositoryMock = new Mock<IFlightRepository>();
            _passengerRepositoryMock = new Mock<IPassengerRepository>();
            _clientMock = new Mock<IRequestClient<BookingCreatedEvent>>();
            _handler = new CreateBookingHandler(_flightRepositoryMock.Object, _passengerRepositoryMock.Object, _clientMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_Booking()
        {
            // Arrange
            var command = new CreateBookingCommand
            {
                FlightId = 1,
                PassengerId = 1,
                SeatCount = 2
            };

            var flight = new Flight
            {
                Id = command.FlightId,
                FlightNumber = "AB123",
                Origin = "JFK",
                Destination = "LAX",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(6),
                AvailableSeats = 100,
                Price = 299.99M
            };

            var passenger = new Passenger
            {
                Id = command.PassengerId,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PassportNumber = "123456789"
            };

            var booking = new Booking
            {
                Id = 0,
                FlightId = command.FlightId,
                PassengerId = command.PassengerId,
                BookingDate = DateTime.Now,
                SeatCount = command.SeatCount
            };

            _flightRepositoryMock.Setup(x => x.GetByIdAsync(command.FlightId)).ReturnsAsync(flight);
            _passengerRepositoryMock.Setup(x => x.GetByIdAsync(command.PassengerId)).ReturnsAsync(passenger);
            _clientMock.Setup(x => x.GetResponse<BookingCreatedEventResponse>(It.IsAny<BookingCreatedEvent>(), It.IsAny<CancellationToken>(), default(RequestTimeout)))
                       .ReturnsAsync(Mock.Of<Response<BookingCreatedEventResponse>>);


            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _flightRepositoryMock.Verify(x => x.GetByIdAsync(command.FlightId), Times.Once);
            _passengerRepositoryMock.Verify(x => x.GetByIdAsync(command.PassengerId), Times.Once);
            _clientMock.Verify(x => x.GetResponse<BookingCreatedEventResponse>(It.IsAny<BookingCreatedEvent>(), It.IsAny<CancellationToken>(), default(RequestTimeout)), Times.Once);
            Assert.Equal(command.FlightId, result.FlightId);
            Assert.Equal(command.PassengerId, result.PassengerId);
            Assert.Equal(command.SeatCount, result.SeatCount);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Flight_Or_Passenger_Not_Found()
        {
            // Arrange
            var command = new CreateBookingCommand
            {
                FlightId = 400,
                PassengerId = 1,
                SeatCount = 2
            };

            _flightRepositoryMock.Setup(x => x.GetByIdAsync(command.FlightId)).ReturnsAsync((Flight)null);
            _passengerRepositoryMock.Setup(x => x.GetByIdAsync(command.PassengerId)).ReturnsAsync((Passenger)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _flightRepositoryMock.Verify(x => x.GetByIdAsync(command.FlightId), Times.Once);
            _passengerRepositoryMock.Verify(x => x.GetByIdAsync(command.PassengerId), Times.Once);
            _clientMock.Verify(x => x.GetResponse<BookingCreatedEventResponse>(It.IsAny<BookingCreatedEvent>(), It.IsAny<CancellationToken>(), default(RequestTimeout)), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_No_Available_Seats()
        {
            // Arrange
            var command = new CreateBookingCommand
            {
                FlightId = 1,
                PassengerId = 1,
                SeatCount = 2
            };

            var flight = new Flight
            {
                Id = command.FlightId,
                FlightNumber = "AB123",
                Origin = "JFK",
                Destination = "LAX",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(6),
                AvailableSeats = 1,
                Price = 299.99M
            };

            var passenger = new Passenger
            {
                Id = command.PassengerId,
                FullName = "John Doe",
                Email = "john.doe@example.com",
                PassportNumber = "123456789"
            };

            _flightRepositoryMock.Setup(x => x.GetByIdAsync(command.FlightId)).ReturnsAsync(flight);
            _passengerRepositoryMock.Setup(x => x.GetByIdAsync(command.PassengerId)).ReturnsAsync(passenger);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _flightRepositoryMock.Verify(x => x.GetByIdAsync(command.FlightId), Times.Once);
            _passengerRepositoryMock.Verify(x => x.GetByIdAsync(command.PassengerId), Times.Once);
            _clientMock.Verify(x => x.GetResponse<BookingCreatedEventResponse>(It.IsAny<BookingCreatedEvent>(), It.IsAny<CancellationToken>(), default(RequestTimeout)), Times.Once);
        }

        [Theory]
        [InlineData(0, 1, 2)]
        [InlineData(1, 0, 2)]
        [InlineData(1, 1, 0)]
        public async Task Handle_Should_Throw_Exception_For_Invalid_Command(int flightId, int passengerId, int seatCount)
        {
            // Arrange
            var command = new CreateBookingCommand
            {
                FlightId = flightId,
                PassengerId = passengerId,
                SeatCount = seatCount
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _flightRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _passengerRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Never);
            _clientMock.Verify(x => x.GetResponse<BookingCreatedEventResponse>(It.IsAny<BookingCreatedEvent>(), It.IsAny<CancellationToken>(), default(RequestTimeout)), Times.Once);
        }
    }
}
