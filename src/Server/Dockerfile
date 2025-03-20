FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StorekeeperAssistant.Web/StorekeeperAssistant.Web.csproj", "StorekeeperAssistant.Web/"]
COPY ["BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks.Infrastructure/"]
COPY ["BuildingBlocks.UseCases/BuildingBlocks.UseCases.csproj", "BuildingBlocks.UseCases/"]
COPY ["StorekeeperAssistant.DataAccess.MsSql/StorekeeperAssistant.DataAccess.csproj", "StorekeeperAssistant.DataAccess.MsSql/"]
COPY ["StorekeeperAssistant.Domain/StorekeeperAssistant.Domain.csproj", "StorekeeperAssistant.Domain/"]
COPY ["BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks.Domain/"]
COPY ["StorekeeperAssistant.UseCases/StorekeeperAssistant.UseCases.csproj", "StorekeeperAssistant.UseCases/"]
RUN dotnet restore "StorekeeperAssistant.Web/StorekeeperAssistant.Web.csproj"
COPY . .
WORKDIR "StorekeeperAssistant.Web"
RUN dotnet build "StorekeeperAssistant.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StorekeeperAssistant.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorekeeperAssistant.Web.dll"]