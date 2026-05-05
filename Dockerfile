# Base runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj (agora dentro de /src)
COPY ["src/NecroFinances.API/NecroFinances.API.csproj", "src/NecroFinances.API/"]
COPY ["src/NecroFinances.Application/NecroFinances.Application.csproj", "src/NecroFinances.Application/"]
COPY ["src/NecroFinances.Infrastructure/NecroFinances.Infrastructure.csproj", "src/NecroFinances.Infrastructure/"]

# Restore
RUN dotnet restore "src/NecroFinances.API/NecroFinances.API.csproj"

# Copy everything
COPY . .

# Build
RUN dotnet build "src/NecroFinances.API/NecroFinances.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "src/NecroFinances.API/NecroFinances.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NecroFinances.API.dll"]