#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0  AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

EXPOSE 443
EXPOSE 8080

COPY ./src/RoaylLibrary.Data/RoaylLibrary.Data.csproj /src/RoaylLibrary.Data
COPY ./src/RoaylLibrary.Domain/RoaylLibrary.Domain.csproj /src/RoaylLibrary.Domain
COPY ./src/RoyalLibrary.Features/RoyalLibrary.Features.csproj /src/RoyalLibrary.Features
COPY ./src/RoyalLibrary.Api/RoyalLibrary.Api.csproj /src/RoyalLibrary.Api

COPY . .

FROM build AS publish
WORKDIR src/RoyalLibrary.Api
RUN dotnet publish "RoyalLibrary.Api.csproj" -c Release -o /app/publish

FROM base AS final

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RoyalLibrary.Api.dll"]