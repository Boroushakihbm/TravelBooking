namespace TravelBooking.Common.Commands.Passenger.Validators;

public class UpdatePassengerValidator : AbstractValidator<UpdatePassengerCommand>
{
    public UpdatePassengerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Id is invalid.");

        RuleFor(x => x.FullName)
                    .NotEmpty()
                    .WithMessage("Full name cannot be empty.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email cannot be empty.")
            .EmailAddress()
            .WithMessage("Email format is invalid.");

        RuleFor(x => x.PassportNumber)
            .NotEmpty()
            .WithMessage("Passport number cannot be empty.");

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{10,11}$")
            .When(x => x.PhoneNumber != null)
            .WithMessage("Phone number format is invalid.");
    }
}