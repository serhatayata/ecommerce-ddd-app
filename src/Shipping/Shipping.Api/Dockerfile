# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/Shipping/Shipping.Api/Shipping.Api.csproj", "src/Shipping/Shipping.Api/"]
COPY ["src/Shipping/Shipping.Application/Shipping.Application.csproj", "src/Shipping/Shipping.Application/"]
COPY ["src/Shipping/Shipping.Domain/Shipping.Domain.csproj", "src/Shipping/Shipping.Domain/"]
COPY ["src/Shipping/Shipping.Infrastructure/Shipping.Infrastructure.csproj", "src/Shipping/Shipping.Infrastructure/"]
COPY ["src/Shipping/Shipping.IoC/Shipping.IoC.csproj", "src/Shipping/Shipping.IoC/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/Shipping/Shipping.Api/Shipping.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/Shipping/Shipping.Api"
RUN dotnet build "Shipping.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Shipping.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Shipping.Api.dll"]
