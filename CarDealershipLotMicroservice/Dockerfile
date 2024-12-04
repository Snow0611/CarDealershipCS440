FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarDealershipLotMicroservice/CarDealershipLotMicroservice.csproj", "CarDealershipLotMicroservice/"]
RUN dotnet restore "CarDealershipLotMicroservice/CarDealershipLotMicroservice.csproj"

COPY . .
WORKDIR "/src/CarDealershipLotMicroservice"
RUN dotnet publish "CarDealershipLotMicroservice.csproj" -c Release -o /app/publish

WORKDIR /app
COPY --from=publish /app/build .
ENTRYPOINT ["dotnet", "CarDealershipLotMicroservice.dll"]
