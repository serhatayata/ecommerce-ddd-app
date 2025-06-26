# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/PaymentSystem/PaymentSystem.Api/PaymentSystem.Api.csproj", "src/PaymentSystem/PaymentSystem.Api/"]
COPY ["src/PaymentSystem/PaymentSystem.Application/PaymentSystem.Application.csproj", "src/PaymentSystem/PaymentSystem.Application/"]
COPY ["src/PaymentSystem/PaymentSystem.Domain/PaymentSystem.Domain.csproj", "src/PaymentSystem/PaymentSystem.Domain/"]
COPY ["src/PaymentSystem/PaymentSystem.Infrastructure/PaymentSystem.Infrastructure.csproj", "src/PaymentSystem/PaymentSystem.Infrastructure/"]
COPY ["src/PaymentSystem/PaymentSystem.IoC/PaymentSystem.IoC.csproj", "src/PaymentSystem/PaymentSystem.IoC/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/PaymentSystem/PaymentSystem.Api/PaymentSystem.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/PaymentSystem/PaymentSystem.Api"
RUN dotnet build "PaymentSystem.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "PaymentSystem.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "PaymentSystem.Api.dll"]
