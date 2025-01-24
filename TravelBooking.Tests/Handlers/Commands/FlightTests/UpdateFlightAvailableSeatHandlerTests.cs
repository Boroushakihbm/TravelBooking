using Moq;
using TravelBooking.Application.Handlers.Commands.Flight;
using TravelBooking.Common.Commands.Flight;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Tests.Handlers.Commands.FlightTests
{
    public class UpdateFlightAvailableSeatHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly UpdateFlightAvailableSeatHandler _handler;

        public UpdateFlightAvailableSeatHandlerTests()
        {
            _flightRepositoryMock = new Mock<IFlightRepository>();
            _handler = new UpdateFlightAvailableSeatHandler(_flightRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Update_AvailableSeats()
        {
            // Arrange
            var command = new UpdateFlightAvailableSeatCommand
            {
                FlightNumber = "AB123",
                AvailableSeats = 50
            };

            var flight = new Flight
            {
                Id = 1,
                FlightNumber = command.FlightNumber,
                Origin = "JFK",
                Destination = "LAX",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(6),
                AvailableSeats = 100,
                Price = 299.99M
            };

            _flightRepositoryMock.Setup(x => x.GetByFlightNumberAsync(command.FlightNumber, CancellationToken.None)).ReturnsAsync(flight);
            _flightRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Flight>(), CancellationToken.None)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _flightRepositoryMock.Verify(x => x.GetByFlightNumberAsync(command.FlightNumber, CancellationToken.None), Times.Once);
            _flightRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Flight>(), CancellationToken.None), Times.Once);
            Assert.NotNull(result);
            Assert.Equal(command.AvailableSeats, result.AvailableSeats);
        }

        [Fact]
        public async Task Handle_Should_Throw_Exception_When_Flight_Not_Found()
        {
            // Arrange
            var command = new UpdateFlightAvailableSeatCommand
            {
                FlightNumber = "AB123",
                AvailableSeats = 50
            };

            _flightRepositoryMock.Setup(x => x.GetByFlightNumberAsync(command.FlightNumber, CancellationToken.None)).ReturnsAsync((Flight)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            _flightRepositoryMock.Verify(x => x.GetByFlightNumberAsync(command.FlightNumber, CancellationToken.None), Times.Once);
            _flightRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Flight>(), CancellationToken.None), Times.Never);
        }
    }
}

