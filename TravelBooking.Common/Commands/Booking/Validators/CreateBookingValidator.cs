namespace TravelBooking.Common.Commands.Booking.Validators;

public class CreateBookingValidator : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingValidator()
    {
        RuleFor(x => x.PassengerId)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Id is invalid.");

        RuleFor(x => x.SeatCount)
                    .NotEmpty()
                    .WithMessage("Seat count cannot be empty.");

        RuleFor(x => x.FlightId)
                    .NotEmpty()
                    .WithMessage("Flight id cannot be empty.");
    }
}

