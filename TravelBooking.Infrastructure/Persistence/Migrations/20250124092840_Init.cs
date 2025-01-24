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

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<int>(type: "int", nullable: false),
                    PassengerId = table.Column<int>(type: "int", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalTime", "AvailableSeats", "DepartureTime", "Destination", "FlightNumber", "Origin", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 24, 16, 58, 39, 245, DateTimeKind.Local).AddTicks(192), 100, new DateTime(2025, 1, 24, 14, 58, 39, 243, DateTimeKind.Local).AddTicks(7124), "Mashhad", "AB123", "Tehran", 200.00m },
                    { 2, new DateTime(2025, 1, 24, 19, 58, 39, 245, DateTimeKind.Local).AddTicks(1129), 150, new DateTime(2025, 1, 24, 17, 58, 39, 245, DateTimeKind.Local).AddTicks(1124), "Kish", "CD456", "Tehran", 300.00m }
                });

            migrationBuilder.InsertData(
                table: "Passengers",
                columns: new[] { "Id", "Email", "FullName", "PassportNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "test1@gmail.com", "Pooyan Boroushaki", "A1234567", "09356092381" },
                    { 2, "test2@gmail.com", "Saman Ahmadi", "B7654321", "09356092382" }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BookingDate", "FlightId", "PassengerId", "SeatCount" },
                values: new object[,]
                {
                    { new Guid("16e455a4-5600-49a5-8df8-f88a579626d1"), new DateTime(2025, 1, 24, 12, 58, 39, 246, DateTimeKind.Local).AddTicks(389), 1, 1, 1 },
                    { new Guid("e0f90fcd-3b65-4526-b22f-056b2f0cc3bf"), new DateTime(2025, 1, 24, 12, 58, 39, 246, DateTimeKind.Local).AddTicks(861), 2, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_PassengerId",
                table: "Bookings",
                column: "PassengerId");
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
