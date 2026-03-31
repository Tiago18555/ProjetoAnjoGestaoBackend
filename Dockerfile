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

WORKDIR /app
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish /src/ProjetoAnjoGestaoBackend/ProjetoAnjoGestaoBackend.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false
COPY --from=build /src /src

ENTRYPOINT ["/bin/sh", "-c", "dotnet ProjetoAnjoGestaoBackend.dll"]
