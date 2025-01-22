using MediatR;
using TravelBooking.Common.Commands.Passenger;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Commands.Passenger;

public class CreatePassengerHandler : IRequestHandler<CreatePassengerCommand, Domain.Entities.Passenger>
{
    private readonly IPassengerRepository _repository;

    public CreatePassengerHandler(IPassengerRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Passenger> Handle(CreatePassengerCommand request, CancellationToken cancellationToken)
    {
        var Passenger = new Domain.Entities.Passenger
        {
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            PassportNumber = request.PassportNumber,
            FullName = request.FullName
        };
        await _repository.AddAsync(Passenger);
        return Passenger;
    }
}
