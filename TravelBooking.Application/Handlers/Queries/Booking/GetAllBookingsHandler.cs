using AutoMapper;
using MediatR;
using TravelBooking.Common.DTOs.BokingDTOs;
using TravelBooking.Common.Queries.Booking;
using TravelBooking.Domain.Interfaces;

namespace TravelBooking.Application.Handlers.Queries.Booking;

public class GetAllBookingsHandler : IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingDTO>?>
{
    private readonly IBookingRepository _repository;
    private readonly IMapper _mapper;

    public GetAllBookingsHandler(IBookingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookingDTO>?> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
    {
        var dynamicList = await _repository.GetBookingsAsync(request.FlightNumber ?? string.Empty, request.take, request.skip);
        var bookingDtos = _mapper.Map<IEnumerable<BookingDTO>>(dynamicList);
        return bookingDtos;
    }
}
