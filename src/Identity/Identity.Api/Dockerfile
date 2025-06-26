# Use the .NET 9.0 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["src/Identity/Identity.Api/Identity.Api.csproj", "src/Identity/Identity.Api/"]
COPY ["src/Identity/Identity.Application/Identity.Application.csproj", "src/Identity/Identity.Application/"]
COPY ["src/Identity/Identity.Domain/Identity.Domain.csproj", "src/Identity/Identity.Domain/"]
COPY ["src/Identity/Identity.Infrastructure/Identity.Infrastructure.csproj", "src/Identity/Identity.Infrastructure/"]
COPY ["src/Identity/Identity.IoC/Identity.IoC.csproj", "src/Identity/Identity.IoC/"]
COPY ["src/Common/Common.Api/Common.Api.csproj", "src/Common/Common.Api/"]
COPY ["src/Common/Common.Application/Common.Application.csproj", "src/Common/Common.Application/"]
COPY ["src/Common/Common.Domain/Common.Domain.csproj", "src/Common/Common.Domain/"]
COPY ["src/Common/Common.Infrastructure/Common.Infrastructure.csproj", "src/Common/Common.Infrastructure/"]

RUN dotnet restore "src/Identity/Identity.Api/Identity.Api.csproj"

# Copy everything and build
COPY . .
WORKDIR "/src/src/Identity/Identity.Api"
RUN dotnet build "Identity.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "Identity.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
EXPOSE 8081
ENTRYPOINT ["dotnet", "Identity.Api.dll"]
