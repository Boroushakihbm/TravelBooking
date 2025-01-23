using AutoMapper;
using TravelBooking.Common.DTOs.BokingDTOs;

namespace TravelBooking.Common.AutoMappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<dynamic, BookingDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest) => ConvertToNullableInt(src, "Id")))
            .ForMember(dest => dest.SeatCount, opt => opt.MapFrom((src, dest) => ConvertToNullableInt(src, "SeatCount")))
            .ForMember(dest => dest.Origin, opt => opt.MapFrom((src, dest) => ConvertToNullableString(src, "Origin")))
            .ForMember(dest => dest.Destination, opt => opt.MapFrom((src, dest) => ConvertToNullableString(src, "Destination")))
            .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom((src, dest) => ConvertToNullableDateTime(src, "DepartureTime")))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom((src, dest) => ConvertToNullableString(src, "FullName")))
            .ForMember(dest => dest.BookingDate, opt => opt.MapFrom((src, dest) => ConvertToNullableDateTime(src, "BookingDate")));
    }

    private int? ConvertToNullableInt(dynamic source, string propertyName)
    {
        var value = GetPropertyValue(source, propertyName);
        return value != null ? (int?)Convert.ToInt32(value) : null;
    }

    private string? ConvertToNullableString(dynamic source, string propertyName)
    {
        var value = GetPropertyValue(source, propertyName);
        return value != null ? (string?)Convert.ToString(value) : null;
    }

    private DateTime? ConvertToNullableDateTime(dynamic source, string propertyName)
    {
        var value = GetPropertyValue(source, propertyName);
        return value != null ? (DateTime?)Convert.ToDateTime(value) : null;
    }

    private object? GetPropertyValue(dynamic source, string propertyName)
    {
        var propertyInfo = source.GetType().GetProperty(propertyName);
        return propertyInfo != null ? propertyInfo.GetValue(source, null) : null;
    }
}


