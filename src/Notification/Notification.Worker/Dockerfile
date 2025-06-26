# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/Notification/Notification.Worker/Notification.Worker.csproj", "src/Notification/Notification.Worker/"]
COPY ["src/Common/Common.Worker/Common.Worker.csproj", "src/Common/Common.Worker/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/Notification/Notification.Worker/Notification.Worker.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/Notification/Notification.Worker"
RUN dotnet build "Notification.Worker.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Notification.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image (not just runtime)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notification.Worker.dll"]
