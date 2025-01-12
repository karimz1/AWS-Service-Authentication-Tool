# !bin/bash
dotnet pack --configuration Release
cd nupkg/
dotnet nuget add source $(pwd) --name LocalNuGet
dotnet nuget list source
dotnet tool install --global awsat --add-source $(pwd)
echo 'export PATH="$PATH:/home/vscode/.dotnet/tools"' >> ~/.bashrc
awsat --help
