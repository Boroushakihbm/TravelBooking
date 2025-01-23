namespace TravelBooking.Common.Commands.Flight.Validators;

public class UpdateFlightAvailableSeatValidator : AbstractValidator<UpdateFlightAvailableSeatCommand>
{
    public UpdateFlightAvailableSeatValidator()
    {
        RuleFor(x => x.AvailableSeats)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Id is invalid.");

        RuleFor(x => x.FlightNumber)
                    .NotEmpty()
                    .WithMessage("Flight number cannot be empty.");
       
    }
}