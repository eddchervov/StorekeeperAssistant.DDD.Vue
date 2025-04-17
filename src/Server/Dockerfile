FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Server/StorekeeperAssistant.Web/StorekeeperAssistant.Web.csproj", "StorekeeperAssistant.Web/"]
COPY ["Server/BuildingBlocks.Infrastructure/BuildingBlocks.Infrastructure.csproj", "BuildingBlocks.Infrastructure/"]
COPY ["Server/BuildingBlocks.UseCases/BuildingBlocks.UseCases.csproj", "BuildingBlocks.UseCases/"]
COPY ["Server/StorekeeperAssistant.DataAccess.MsSql/StorekeeperAssistant.DataAccess.csproj", "StorekeeperAssistant.DataAccess.MsSql/"]
COPY ["Server/StorekeeperAssistant.Domain/StorekeeperAssistant.Domain.csproj", "StorekeeperAssistant.Domain/"]
COPY ["Server/BuildingBlocks.Domain/BuildingBlocks.Domain.csproj", "BuildingBlocks.Domain/"]
COPY ["Server/StorekeeperAssistant.UseCases/StorekeeperAssistant.UseCases.csproj", "StorekeeperAssistant.UseCases/"]
RUN dotnet restore "StorekeeperAssistant.Web/StorekeeperAssistant.Web.csproj"
COPY . .
WORKDIR "Server/StorekeeperAssistant.Web"
RUN dotnet build "StorekeeperAssistant.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StorekeeperAssistant.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StorekeeperAssistant.Web.dll"]