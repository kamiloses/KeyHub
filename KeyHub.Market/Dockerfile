FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["KeyHub.Market.csproj", "./"]
RUN dotnet restore "KeyHub.Market.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "KeyHub.Market.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN dotnet publish "KeyHub.Market.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish ./
ENTRYPOINT ["dotnet", "KeyHub.Market.dll"]
