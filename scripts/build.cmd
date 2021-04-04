@echo off 

REM Clean (clean because of issues with builds often)dotnet restore Tempo.sln
dotnet clean "Tempo.sln" --nologo

REM Build
dotnet build "Tempo.sln" --nologo || exit /b

REM dotnet test "..\src\Tempo.Tests" --nologo --no-build