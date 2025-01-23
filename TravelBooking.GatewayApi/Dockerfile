# Use the official .NET 8.0 SDK image as a build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the csproj and restore as distinct layers
COPY *.sln .
COPY TravelBooking.GatewayApi/*.csproj ./TravelBooking.GatewayApi/
COPY TravelBooking.Application/*.csproj ./TravelBooking.Application/
COPY TravelBooking.Domain/*.csproj ./TravelBooking.Domain/
COPY TravelBooking.Infrastructure/*.csproj ./TravelBooking.Infrastructure/
COPY TravelBooking.Common/*.csproj ./TravelBooking.Common/
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/TravelBooking.GatewayApi
RUN dotnet publish -c Release -o out

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/TravelBooking.GatewayApi/out ./
ENTRYPOINT ["dotnet", "TravelBooking.GatewayApi.dll"]
