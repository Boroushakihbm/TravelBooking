namespace TravelBooking.Common.Commands.Flight.Validators;

public class CreateFlightValidator : AbstractValidator<CreateFlightCommand>
{
    public CreateFlightValidator()
    {
        RuleFor(x => x.AvailableSeats)
            .NotEmpty()
            .WithMessage("Available seats cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Available seats is invalid.");

        RuleFor(x => x.FlightNumber)
            .NotEmpty()
            .WithMessage("Flight number cannot be empty.");

        RuleFor(x => x.DepartureTime)
            .NotEmpty()
            .WithMessage("Departure time cannot be empty.");

        RuleFor(x => x.ArrivalTime)
            .NotEmpty()
            .WithMessage("Arrival time cannot be empty.");

        RuleFor(x => x.Price)
            .NotEmpty()
            .WithMessage("Price cannot be empty.");
    }
}