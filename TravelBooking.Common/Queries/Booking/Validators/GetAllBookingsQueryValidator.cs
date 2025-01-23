namespace TravelBooking.Common.Queries.Booking.Validators;

public class GetAllBookingsQueryValidator : AbstractValidator<GetAllBookingsQuery>
{
    public GetAllBookingsQueryValidator()
    {
        RuleFor(x => x.take)
            .GreaterThan(0)
            .WithMessage("The number of items to take must be greater than zero.");

        RuleFor(x => x.skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The number of items to skip must be zero or greater.");

        RuleFor(x => x.FlightNumber)
            .NotNull()
            .WithMessage("Flight number should not be null.")
            .MaximumLength(10)
            .WithMessage("Flight number should not exceed 10 characters.")
            .When(x => !string.IsNullOrEmpty(x.FlightNumber));
    }
}
