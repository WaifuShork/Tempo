@echo off 

REM Restore, Clean & Build
dotnet restore Tempo.sln
dotnet clean Tempo.sln
dotnet build Tempo.sln