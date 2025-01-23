namespace TravelBooking.Common.Commands.Flight.Validators;

public class DeleteFlightValidator : AbstractValidator<DeleteFlightCommand>
{
    public DeleteFlightValidator()
    {
        RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("Id cannot be empty.");
    }
}