namespace TravelBooking.Common.Queries.Passenger.Validators;

public class GetPassengerByIdQueryValidator : AbstractValidator<GetPassengerByIdQuery>
{
    public GetPassengerByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Passenger ID must be provided.")
            .GreaterThan(0)
            .WithMessage("Passenger ID must be greater than zero.");
    }
}