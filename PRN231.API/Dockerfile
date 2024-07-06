#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["PRN231.API/PRN231.API.csproj", "PRN231.API/"]
COPY ["PRN231.Application/PRN231.Application.csproj", "PRN231.Application/"]
COPY ["PRN231.Domain/PRN231.Domain.csproj", "PRN231.Domain/"]
COPY ["PRN231.Infrastructure/PRN231.Infrastructure.csproj", "PRN231.Infrastructure/"]
COPY ["PRN231.EntityFrameworkCore/PRN231.EntityFrameworkCore.csproj", "PRN231.EntityFrameworkCore/"]
RUN dotnet restore "./PRN231.API/./PRN231.API.csproj"
COPY . .
WORKDIR "/src/PRN231.API"
RUN dotnet build "./PRN231.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./PRN231.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PRN231.API.dll"]