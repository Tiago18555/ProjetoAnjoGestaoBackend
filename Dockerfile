FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

# COPY SRC
WORKDIR /src

COPY ["ProjetoAnjoGestaoBackend.sln", "./"]

COPY ["ProjetoAnjoGestaoBackend/ProjetoAnjoGestaoBackend.csproj", "ProjetoAnjoGestaoBackend/"]
COPY ["ProjetoAnjoGestaoBackend.Tests/ProjetoAnjoGestaoBackend.Tests.csproj", "ProjetoAnjoGestaoBackend.Tests/"]

COPY ["./ProjetoAnjoGestaoBackend/appsettings.Docker.json", "./ProjetoAnjoGestaoBackend"]

# RESTORE & COPY
RUN dotnet restore "ProjetoAnjoGestaoBackend.sln"
COPY . .

# BUILD & TEST
RUN dotnet build "ProjetoAnjoGestaoBackend/ProjetoAnjoGestaoBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build
RUN dotnet test "ProjetoAnjoGestaoBackend.Tests/ProjetoAnjoGestaoBackend.Tests.csproj" -c Debug

# PUBLISH
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/ProjetoAnjoGestaoBackend
RUN dotnet publish "ProjetoAnjoGestaoBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# RUN
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProjetoAnjoGestaoBackend.dll"]