using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;
using TravelBooking.Infrastructure.mssql.Repositories;
using MediatR;
using System.Reflection;
using TravelBooking.GatewayApi.Configuration;
using TravelBooking.Application.Handlers.Queries.Passenger;
using TravelBooking.Common.AutoMappers;
using TravelBooking.Common.Commands.Passenger.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using TravelBooking.GatewayApi.Middlewaries;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TravelBookingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();

builder.Services.AddMediatR(typeof(GetPassengerByIdHandler).GetTypeInfo().Assembly);

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddValidatorsFromAssemblyContaining<CreatePassengerValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddMemoryCache();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidateModelFilter>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MigrateDatabase();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();