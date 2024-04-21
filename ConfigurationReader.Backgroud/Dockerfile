#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
EXPOSE 5672
EXPOSE 1433

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ConfigurationReader.Backgroud/ConfigurationReader.Backgroud.csproj", "ConfigurationReader.Backgroud/"]
RUN dotnet restore "./ConfigurationReader.Backgroud/./ConfigurationReader.Backgroud.csproj"
COPY . .
WORKDIR "/src/ConfigurationReader.Backgroud"
RUN dotnet build "./ConfigurationReader.Backgroud.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ConfigurationReader.Backgroud.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ConfigurationReader.Backgroud.dll"]