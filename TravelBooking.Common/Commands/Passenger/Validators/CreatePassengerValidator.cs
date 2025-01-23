using FluentValidation;

namespace TravelBooking.Common.Commands.Passenger.Validators;

public class CreatePassengerValidator : AbstractValidator<CreatePassengerCommand>
{
    public CreatePassengerValidator()
    {
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
