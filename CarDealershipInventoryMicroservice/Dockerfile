FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarDealershipInventoryMicroservice/CarDealershipCarMicroservice.csproj", "CarDealershipInventoryMicroservice/"]
RUN dotnet restore "CarDealershipInventoryMicroservice/CarDealershipCarMicroservice.csproj"

COPY . .
WORKDIR "/src/CarDealershipInventoryMicroservice"
RUN dotnet publish "CarDealershipCarMicroservice.csproj" -c Release -o /app/publish 

WORKDIR /app
COPY --from=publish /app/build .
ENTRYPOINT ["dotnet", "CarDealershipCarMicroservice.dll"]
