#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 81

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConfigurationReader.WebApp/ConfigurationReader.WebApp.csproj", "ConfigurationReader.WebApp/"]
COPY ["ConfigurationReader.Shareds/ConfigurationReader.Shared.csproj", "ConfigurationReader.Shareds/"]
RUN dotnet restore "./ConfigurationReader.WebApp/./ConfigurationReader.WebApp.csproj"
COPY . .
WORKDIR "/src/ConfigurationReader.WebApp"
RUN dotnet build "./ConfigurationReader.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConfigurationReader.WebApp.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurationReader.WebApp.dll"]