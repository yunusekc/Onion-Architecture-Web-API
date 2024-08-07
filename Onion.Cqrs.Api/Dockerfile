#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Onion.Cqrs.Api/Onion.Cqrs.Api.csproj", "Onion.Cqrs.Api/"]
COPY ["Onion.Cqrs.Application/Onion.Cqrs.Application.csproj", "Onion.Cqrs.Application/"]
COPY ["Onion.Cqrs.Domain/Onion.Cqrs.Domain.csproj", "Onion.Cqrs.Domain/"]
COPY ["Onion.Cqrs.Persistence/Onion.Cqrs.Persistence.csproj", "Onion.Cqrs.Persistence/"]
RUN dotnet restore "./Onion.Cqrs.Api/Onion.Cqrs.Api.csproj"
COPY . .
WORKDIR "/src/Onion.Cqrs.Api"
RUN dotnet build "./Onion.Cqrs.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Onion.Cqrs.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Onion.Cqrs.Api.dll"]