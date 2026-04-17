FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

# COPY SRC
WORKDIR /src/ProjetoAnjoGestaoBackend
COPY ["./ProjetoAnjoGestaoBackend.sln", "./"]
COPY ["./ProjetoAnjoGestaoBackend.csproj", "./"]
COPY ["./appsettings.Docker.json", "./"]


# RESTORE
RUN dotnet restore
COPY . .

# BUILD & TEST
#RUN dotnet build "./ProjetoAnjoGestaoBackend.csproj" -c $BUILD_CONFIGURATION -o /app/build
#RUN dotnet test ./Tests -c Debug

# RUN PUBLISH
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
WORKDIR /src/ProjetoAnjoGestaoBackend

RUN dotnet publish "ProjetoAnjoGestaoBackend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
WORKDIR /app/publish
ENTRYPOINT ["dotnet", "ProjetoAnjoGestaoBackend.dll"]