FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "src/ToDeFerias.Bookings.sln" && dotnet build "src/ToDeFerias.Bookings.sln" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/ToDeFerias.Bookings.Api/ToDeFerias.Bookings.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDeFerias.Bookings.Api.dll"]