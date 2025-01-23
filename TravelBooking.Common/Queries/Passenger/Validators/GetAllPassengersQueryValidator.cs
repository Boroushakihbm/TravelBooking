namespace TravelBooking.Common.Queries.Passenger.Validators;

public class GetAllPassengersQueryValidator : AbstractValidator<GetAllPassengersQuery>
{
    public GetAllPassengersQueryValidator()
    {
        RuleFor(x => x.FullName)
            .MaximumLength(50)
            .WithMessage("Full name should not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.FullName));

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email format is invalid.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.PassportNumber)
            .MaximumLength(20)
            .WithMessage("Passport number should not exceed 20 characters.")
            .When(x => !string.IsNullOrEmpty(x.PassportNumber));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^\+?[1-9]\d{10,11}$")
            .WithMessage("Phone number format is invalid.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));
    }
}

