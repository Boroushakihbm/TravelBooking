using Moq;
using TravelBooking.Application.Handlers.Commands.Flight;
using TravelBooking.Common.Commands.Flight;
using TravelBooking.Domain.Entities;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Tests.Handlers.Commands.FlightTests
{
    public class CreateFlightHandlerTests
    {
        private readonly Mock<IFlightRepository> _flightRepositoryMock;
        private readonly CreateFlightHandler _handler;

        public CreateFlightHandlerTests()
        {
            _flightRepositoryMock = new Mock<IFlightRepository>();
            _handler = new CreateFlightHandler(_flightRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_Should_Create_Flight()
        {
            // Arrange
            var command = new CreateFlightCommand
            {
                FlightNumber = "AB123",
                Origin = "JFK",
                Destination = "LAX",
                DepartureTime = DateTime.Now,
                ArrivalTime = DateTime.Now.AddHours(6),
                AvailableSeats = 100,
                Price = 299.99M
            };

            var flight = new Flight
            {
                Id = 1,
                FlightNumber = command.FlightNumber,
                Origin = command.Origin,
                Destination = command.Destination,
                DepartureTime = command.DepartureTime,
                ArrivalTime = command.ArrivalTime,
                AvailableSeats = command.AvailableSeats,
                Price = command.Price
            };

            _flightRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Flight>())).Callback<Flight>(f => f.Id = flight.Id).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _flightRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Flight>()), Times.Once);
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
        }
    }
}
