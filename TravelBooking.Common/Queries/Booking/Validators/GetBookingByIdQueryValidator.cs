namespace TravelBooking.Common.Queries.Booking.Validators;

public class GetBookingByIdQueryValidator : AbstractValidator<GetBookingByIdQuery>
{
    public GetBookingByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotNull()
            .WithMessage("Booking ID must be provided.");
    }
}
