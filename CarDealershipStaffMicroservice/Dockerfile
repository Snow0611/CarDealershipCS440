FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarDealershipStaffMicroservice/CarDealershipStaffMicroservice.csproj", "CarDealershipStaffMicroservice/"]
RUN dotnet restore "CarDealershipStaffMicroservice/CarDealershipStaffMicroservice.csproj"

COPY . .
WORKDIR "/src/CarDealershipStaffMicroservice"
RUN dotnet publish "CarDealershipStaffMicroservice.csproj" -c Release -o /app/publish 

WORKDIR /app
COPY --from=publish /app/build .
ENTRYPOINT ["dotnet", "CarDealershipStaffMicroservice.dll"]
