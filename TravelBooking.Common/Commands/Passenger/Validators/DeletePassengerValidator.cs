namespace TravelBooking.Common.Commands.Passenger.Validators;

public class DeletePassengerValidator : AbstractValidator<DeletePassengerCommand>
{
    public DeletePassengerValidator()
    {
        RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id cannot be empty.");
    }
}
