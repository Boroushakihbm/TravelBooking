# TravelBooking

TravelBooking is a .NET 8.0 based application for managing travel bookings. This project includes multiple components such as a Gateway API, Application layer, Domain layer, Infrastructure layer, and Common utilities.

## Prerequisites

- .NET 8.0 SDK
- SQL Server
- Visual Studio 2022
- Docker

## Project Structure (`DDD` - `CQRS` - Event Sourcing by `RabbitMQ` and `MassTransit`)

- **TravelBooking.GatewayApi**: The API gateway for handling HTTP requests.
- **TravelBooking.Application**: Contains application logic and handlers.
- **TravelBooking.Domain**: Contains domain entities and interfaces.
- **TravelBooking.Infrastructure**: Contains data access logic and repositories.
- **TravelBooking.Common**: Contains common utilities, validators, and DTOs.

## Error handling with proper HTTP status codes and graceful handling of exceptions.
![BadRequest Sample](https://github.com/Boroushakihbm/TravelBooking/blob/master/readmeimg/BadRequest.PNG)
## Unit tests for critical functionalities
![xUnitTest](https://github.com/Boroushakihbm/TravelBooking/blob/master/readmeimg/xUnitTest.PNG)
## Event Sourcing to track changes in flights by RabbitMQ 
![RabbitMQ](https://github.com/Boroushakihbm/TravelBooking/blob/master/readmeimg/RabbitMQ.PNG)
## Setup Instructions

### 1. Clone the Repository


### 2. Configure the Database

Update the connection string in `appsettings.json` file located in `TravelBooking.GatewayApi` project:

{ "ConnectionStrings": { "DefaultConnection": "Server=your_server;Database=TravelBookingDb;User Id=your_user;Password=your_password;" } }


### 3. Apply Migrations

Open the Package Manager Console in Visual Studio and run the following commands to apply migrations:

cd TravelBooking.Infrastructure dotnet ef database update


### 4. Build the Solution

Open the solution in Visual Studio 2022 and build the solution to restore the necessary packages and compile the code.

### 5. Run the Application

Set `TravelBooking.GatewayApi` as the startup project and run the application. The API should be accessible at `https://localhost:5001`.

### 6. Swagger UI

You can access the Swagger UI for API documentation and testing at `https://localhost:5001/swagger`.

## Running Unit Tests

To run the unit tests, open the Test Explorer in Visual Studio and run all tests. The unit tests are located in the `TravelBooking.UnitTests` project.

## Docker Setup

### Dockerfile for TravelBooking.GatewayApi

Create a `Dockerfile` in the `TravelBooking.GatewayApi` project directory:

Use the official .NET 8.0 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build WORKDIR /app

Copy the csproj and restore as distinct layers

COPY .sln . COPY TravelBooking.GatewayApi/.csproj ./TravelBooking.GatewayApi/ COPY TravelBooking.Application/.csproj ./TravelBooking.Application/ COPY TravelBooking.Domain/.csproj ./TravelBooking.Domain/ COPY TravelBooking.Infrastructure/.csproj ./TravelBooking.Infrastructure/ COPY TravelBooking.Common/.csproj ./TravelBooking.Common/ RUN dotnet restore

Copy everything else and build
COPY . . WORKDIR /app/TravelBooking.GatewayApi RUN dotnet publish -c Release -o out

Use the official ASP.NET Core runtime image

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime WORKDIR /app COPY --from=build /app/TravelBooking.GatewayApi/out ./ ENTRYPOINT ["dotnet", "TravelBooking.GatewayApi.dll"]


### docker-compose.yml

Create a `docker-compose.yml` file in the root directory of the project:

version: '3.8'
services: travelbooking-api: build: context: . dockerfile: TravelBooking.GatewayApi/Dockerfile ports: - "5000:80" environment: - ASPNETCORE_ENVIRONMENT=Development - ConnectionStrings__DefaultConnection=Server=sqlserver;Database=TravelBookingDb;User=sa;Password=Your_password123; depends_on: - sqlserver
sqlserver: image: mcr.microsoft.com/mssql/server:2022-latest environment: SA_PASSWORD: "Your_password123" ACCEPT_EULA: "Y" ports: - "1433:1433" volumes: - sqlserverdata:/var/opt/mssql
volumes: sqlserverdata:


### Running the Application with Docker

To build and run the application using Docker, execute the following command in the root directory of the project:

docker-compose up --build

This command will build the Docker images and start the containers. The TravelBooking API should be accessible at `http://localhost:5000`.
