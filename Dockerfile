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

# MIGRATION SETUP
RUN dotnet tool install --global dotnet-ef --version 9.0.0
ENV PATH="$PATH:/root/.dotnet/tools"
RUN dotnet-ef migrations add Initial

# RUN PUBLISH

FROM build AS publish

ARG BUILD_CONFIGURATION=Release
RUN dotnet publish /src/ProjetoAnjoGestaoBackend/ProjetoAnjoGestaoBackend.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM build AS db

WORKDIR /app

#COPY --from=build /root/.dotnet/tools /root/.dotnet/tools/
COPY --from=publish /app/publish ./
COPY --from=build /src /src

COPY run-migrations.sh ./
RUN chmod 777 ./run-migrations.sh

ENTRYPOINT ["/bin/sh", "-c", "./run-migrations.sh && dotnet ProjetoAnjoGestaoBackend.dll"]
