namespace TravelBooking.Common.Commands.Booking.Validators;

public class UpdateBookingValidator : AbstractValidator<UpdateBookingCommand>
{
    public UpdateBookingValidator()
    {
        RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id cannot be empty.");

        RuleFor(x => x.PassengerId)
            .NotEmpty()
            .WithMessage("Passenger Id cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Passenger Id is invalid.");

        RuleFor(x => x.SeatCount)
                    .NotEmpty()
                    .WithMessage("Seat count cannot be empty.");

        RuleFor(x => x.FlightId)
                    .NotEmpty()
                    .WithMessage("Flight id cannot be empty.");
    }
}

