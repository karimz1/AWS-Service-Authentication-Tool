# !bin/bash
dotnet pack --configuration Release
cd nupkg/
dotnet nuget add source $(pwd) --name LocalNuGet
dotnet nuget list source
dotnet tool install --global awsat --version 1.0.1 --add-source $(pwd)
awsat --help
