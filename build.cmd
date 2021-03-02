@echo off 

dotnet restore OpenTK.sln
dotnet clean OpenTK.sln
dotnet build OpenTK.sln

dotnet run --project src/OpenTK.csproj