@echo off 

REM Clean (clean because of issues with builds often)
dotnet clean "src\Tempo\Tempo.csproj" --nologo

REM Build
dotnet build "src\Tempo\Tempo.csproj" --nologo || exit /b 

REM Run
dotnet run --project "src\Tempo\Tempo.csproj" --nologo || exit /b