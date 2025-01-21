using Microsoft.EntityFrameworkCore;
using TravelBooking.Domain.Interfaces;
using TravelBooking.Infrastructure.mssql.Persistence;
using TravelBooking.Infrastructure.mssql.Repositories;
using MediatR;
using System.Reflection;
using TravelBooking.GatewayApi.Configuration;
using TravelBooking.Application.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TravelBookingDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IFlightRepository, FlightRepository>();

builder.Services.AddMediatR(typeof(GetFlightByIdHandler).GetTypeInfo().Assembly);

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

app.Run();