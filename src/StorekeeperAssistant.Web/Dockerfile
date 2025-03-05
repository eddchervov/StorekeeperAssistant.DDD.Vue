#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/StorekeeperAssistant.Web/StorekeeperAssistant.Web.csproj", "src/StorekeeperAssistant.Web/"]
COPY ["src/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "src/BuildingBlocks.Infrastructure/"]
COPY ["src/BuildingBlocks.UseCases/BuildingBlocks.UseCases.csproj", "src/BuildingBlocks.UseCases/"]
COPY ["src/StorekeeperAssistant.DataAccess.MsSql/StorekeeperAssistant.DataAccess.csproj", "src/StorekeeperAssistant.DataAccess.MsSql/"]
COPY ["src/StorekeeperAssistant.Domain/StorekeeperAssistant.Domain.csproj", "src/StorekeeperAssistant.Domain/"]
COPY ["src/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "src/BuildingBlocks.Domain/"]
COPY ["src/StorekeeperAssistant.UseCases/StorekeeperAssistant.UseCases.csproj", "src/StorekeeperAssistant.UseCases/"]
RUN dotnet restore "./src/StorekeeperAssistant.Web/StorekeeperAssistant.Web.csproj"
COPY . .
WORKDIR "/src/src/StorekeeperAssistant.Web"
RUN dotnet build "./StorekeeperAssistant.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StorekeeperAssistant.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorekeeperAssistant.Web.dll"]