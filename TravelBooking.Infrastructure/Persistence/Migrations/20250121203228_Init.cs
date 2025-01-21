using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelBooking.Infrastructure.mssql.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AvailableSeats = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Passengers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passengers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "FlightId", "PassengerId", "SeatNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 22, 0, 2, 27, 953, DateTimeKind.Local).AddTicks(4828), 1, 1, "12A" },
                    { 2, new DateTime(2025, 1, 22, 0, 2, 27, 953, DateTimeKind.Local).AddTicks(5412), 2, 2, "15B" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalTime", "AvailableSeats", "DepartureTime", "Destination", "FlightNumber", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 22, 4, 2, 27, 952, DateTimeKind.Local).AddTicks(2062), 100, new DateTime(2025, 1, 22, 2, 2, 27, 950, DateTimeKind.Local).AddTicks(7473), "Mashhad", "AB123", "Tehran", 200.00m },
                    { 2, new DateTime(2025, 1, 22, 7, 2, 27, 952, DateTimeKind.Local).AddTicks(4182), 150, new DateTime(2025, 1, 22, 5, 2, 27, 952, DateTimeKind.Local).AddTicks(4177), "Kish", "CD456", "Tehran", 300.00m }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Email", "FullName", "PassportNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "test1@gmail.com", "Pooyan Boroushaki", "A1234567", "09356092381" },
                    { 2, "test2@gmail.com", "Saman Ahmadi", "B7654321", "09356092382" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Passengers");
        }
    }
}
