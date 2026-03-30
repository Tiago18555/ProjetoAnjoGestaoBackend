#!/bin/bash

echo Updating migrations
dotnet tool install --global dotnet-ef --version 9.0.0
cd /root/.dotnet/tools
echo now on directory $(pwd)
./dotnet-ef database update --project /src/ProjetoAnjoGestaoBackend/ProjetoAnjoGestaoBackend.csproj --context AppDbContext
echo Run migrations OK