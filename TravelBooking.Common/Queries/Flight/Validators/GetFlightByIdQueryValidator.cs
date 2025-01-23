namespace TravelBooking.Common.Queries.Flight.Validators;

public class GetFlightByIdQueryValidator : AbstractValidator<GetFlightByIdQuery>
{
    public GetFlightByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Flight ID must be provided.")
            .GreaterThan(0)
            .WithMessage("Flight ID must be greater than zero.");
    }
}
