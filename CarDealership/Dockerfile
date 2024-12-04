FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarDealership/CarDealershipAPI.csproj", "CarDealership/"]
RUN dotnet restore "CarDealership/CarDealershipAPI.csproj"

COPY . .
WORKDIR "/src/CarDealership"
RUN dotnet publish "CarDealershipAPI.csproj" -c Release -o /app/publish

WORKDIR /app
COPY --from=publish /app/build .
ENTRYPOINT ["dotnet", "CarDealershipAPI.dll"]
