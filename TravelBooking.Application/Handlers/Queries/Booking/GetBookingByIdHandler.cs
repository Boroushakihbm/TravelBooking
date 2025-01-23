using MediatR;
using TravelBooking.Common.Queries.Booking;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Queries.Booking;

public class GetBookingByIdHandler : IRequestHandler<GetBookingByIdQuery, Domain.Entities.Booking?>
{
    private readonly IBookingRepository _repository;

    public GetBookingByIdHandler(IBookingRepository repository)
    {
        _repository = repository;
    }

    public async Task<Domain.Entities.Booking?> Handle(GetBookingByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id!.Value);
    }
}