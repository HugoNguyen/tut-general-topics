FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["src/API/Evently.Ticketing.Api/Evently.Ticketing.Api.csproj", "src/API/Evently.Ticketing.Api/"]
COPY ["src/Modules/Ticketing/Evently.Modules.Ticketing.Infrastructure/Evently.Modules.Ticketing.Infrastructure.csproj", "src/Modules/Ticketing/Evently.Modules.Ticketing.Infrastructure/"]
COPY ["src/Common/Evently.Common.Infrastructure/Evently.Common.Infrastructure.csproj", "src/Common/Evently.Common.Infrastructure/"]
COPY ["src/Common/Evently.Common.Application/Evently.Common.Application.csproj", "src/Common/Evently.Common.Application/"]
COPY ["src/Common/Evently.Common.Domain/Evently.Common.Domain.csproj", "src/Common/Evently.Common.Domain/"]
COPY ["src/Common/Evently.Common.Presentation/Evently.Common.Presentation.csproj", "src/Common/Evently.Common.Presentation/"]
COPY ["src/Modules/Ticketing/Evently.Modules.Ticketing.Application/Evently.Modules.Ticketing.Application.csproj", "src/Modules/Ticketing/Evently.Modules.Ticketing.Application/"]
COPY ["src/Modules/Ticketing/Evently.Modules.Ticketing.Domain/Evently.Modules.Ticketing.Domain.csproj", "src/Modules/Ticketing/Evently.Modules.Ticketing.Domain/"]
COPY ["src/Modules/Ticketing/Evently.Modules.Ticketing.IntegrationEvents/Evently.Modules.Ticketing.IntegrationEvents.csproj", "src/Modules/Ticketing/Evently.Modules.Ticketing.IntegrationEvents/"]
COPY ["src/Modules/Ticketing/Evently.Modules.Ticketing.Presentation/Evently.Modules.Ticketing.Presentation.csproj", "src/Modules/Ticketing/Evently.Modules.Ticketing.Presentation/"]
COPY ["src/Modules/Events/Evently.Modules.Events.IntegrationEvents/Evently.Modules.Events.IntegrationEvents.csproj", "src/Modules/Events/Evently.Modules.Events.IntegrationEvents/"]
COPY ["src/Modules/Users/Evently.Modules.Users.IntegrationEvents/Evently.Modules.Users.IntegrationEvents.csproj", "src/Modules/Users/Evently.Modules.Users.IntegrationEvents/"]
RUN dotnet restore "./src/API/Evently.Ticketing.Api/Evently.Ticketing.Api.csproj"
COPY . .
WORKDIR "/src/src/API/Evently.Ticketing.Api"
RUN dotnet build "./Evently.Ticketing.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Evently.Ticketing.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Evently.Ticketing.Api.dll"]