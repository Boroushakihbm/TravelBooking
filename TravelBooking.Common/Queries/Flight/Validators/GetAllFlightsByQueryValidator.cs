namespace TravelBooking.Common.Queries.Flight.Validators;

public class GetAllFlightsByQueryValidator : AbstractValidator<GetAllFlightsByQuery>
{
    public GetAllFlightsByQueryValidator()
    {
        RuleFor(x => x.take)
            .GreaterThan(0)
            .WithMessage("The number of items to take must be greater than zero.");

        RuleFor(x => x.skip)
            .GreaterThanOrEqualTo(0)
            .WithMessage("The number of items to skip must be zero or greater.");

        RuleFor(x => x.FlightNumber)
            .MaximumLength(10)
            .WithMessage("Flight number should not exceed 10 characters.")
            .When(x => !string.IsNullOrEmpty(x.FlightNumber));

        RuleFor(x => x.Origin)
            .MaximumLength(50)
            .WithMessage("Origin should not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.Origin));

        RuleFor(x => x.Destination)
            .MaximumLength(50)
            .WithMessage("Destination should not exceed 50 characters.")
            .When(x => !string.IsNullOrEmpty(x.Destination));

        RuleFor(x => x.DepartureTime)
            .LessThan(x => x.ArrivalTime)
            .WithMessage("Departure time must be before arrival time.")
            .When(x => x.DepartureTime.HasValue && x.ArrivalTime.HasValue);

        RuleFor(x => x.AvailableSeats)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Available seats must be zero or greater.")
            .When(x => x.AvailableSeats.HasValue);

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than zero.")
            .When(x => x.Price.HasValue);
    }
}
