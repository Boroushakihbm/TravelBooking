namespace TravelBooking.Common.Commands.Booking.Validators;

public class DeleteBookingValidator : AbstractValidator<DeleteBookingCommand>
{
    public DeleteBookingValidator()
    {
        RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id cannot be empty.");
    }
}

