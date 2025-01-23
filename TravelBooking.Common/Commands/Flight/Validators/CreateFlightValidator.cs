using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBooking.Common.Commands.Passenger;

namespace TravelBooking.Common.Commands.Flight.Validators;

public class CreateFlightValidator : AbstractValidator<CreateFlightCommand>
{
    public CreateFlightValidator()
    {
        RuleFor(x => x.AvailableSeats)
            .NotEmpty()
            .WithMessage("Id cannot be empty.")
            .GreaterThan(0)
            .WithMessage("Id is invalid.");

        RuleFor(x => x.FlightNumber)
                    .NotEmpty()
                    .WithMessage("Flight number cannot be empty.");
    }
}