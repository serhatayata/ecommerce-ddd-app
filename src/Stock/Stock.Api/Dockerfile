# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/Stock/Stock.Api/Stock.Api.csproj", "src/Stock/Stock.Api/"]
COPY ["src/Stock/Stock.Application/Stock.Application.csproj", "src/Stock/Stock.Application/"]
COPY ["src/Stock/Stock.Domain/Stock.Domain.csproj", "src/Stock/Stock.Domain/"]
COPY ["src/Stock/Stock.Infrastructure/Stock.Infrastructure.csproj", "src/Stock/Stock.Infrastructure/"]
COPY ["src/Stock/Stock.IoC/Stock.IoC.csproj", "src/Stock/Stock.IoC/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/Stock/Stock.Api/Stock.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/Stock/Stock.Api"
RUN dotnet build "Stock.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Stock.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Stock.Api.dll"]
