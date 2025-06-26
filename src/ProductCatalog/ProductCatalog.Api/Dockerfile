# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/ProductCatalog/ProductCatalog.Api/ProductCatalog.Api.csproj", "src/ProductCatalog/ProductCatalog.Api/"]
COPY ["src/ProductCatalog/ProductCatalog.Application/ProductCatalog.Application.csproj", "src/ProductCatalog/ProductCatalog.Application/"]
COPY ["src/ProductCatalog/ProductCatalog.Domain/ProductCatalog.Domain.csproj", "src/ProductCatalog/ProductCatalog.Domain/"]
COPY ["src/ProductCatalog/ProductCatalog.Infrastructure/ProductCatalog.Infrastructure.csproj", "src/ProductCatalog/ProductCatalog.Infrastructure/"]
COPY ["src/ProductCatalog/ProductCatalog.IoC/ProductCatalog.IoC.csproj", "src/ProductCatalog/ProductCatalog.IoC/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/ProductCatalog/ProductCatalog.Api/ProductCatalog.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/ProductCatalog/ProductCatalog.Api"
RUN dotnet build "ProductCatalog.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ProductCatalog.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "ProductCatalog.Api.dll"]
