#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["employee-management.api/employee-management.api.csproj", "employee-management.api/"]
RUN dotnet restore "employee-management.api/employee-management.api.csproj"
COPY . .
WORKDIR "/src/employee-management.api"
RUN dotnet build "employee-management.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "employee-management.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "employee-management.api.dll"]