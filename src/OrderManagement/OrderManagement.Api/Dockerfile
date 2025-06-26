# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/OrderManagement/OrderManagement.Api/OrderManagement.Api.csproj", "src/OrderManagement/OrderManagement.Api/"]
COPY ["src/OrderManagement/OrderManagement.Application/OrderManagement.Application.csproj", "src/OrderManagement/OrderManagement.Application/"]
COPY ["src/OrderManagement/OrderManagement.Domain/OrderManagement.Domain.csproj", "src/OrderManagement/OrderManagement.Domain/"]
COPY ["src/OrderManagement/OrderManagement.Infrastructure/OrderManagement.Infrastructure.csproj", "src/OrderManagement/OrderManagement.Infrastructure/"]
COPY ["src/OrderManagement/OrderManagement.IoC/OrderManagement.IoC.csproj", "src/OrderManagement/OrderManagement.IoC/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/OrderManagement/OrderManagement.Api/OrderManagement.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/OrderManagement/OrderManagement.Api"
RUN dotnet build "OrderManagement.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]
