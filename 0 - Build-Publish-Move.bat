@echo off

dotnet restore

dotnet build --no-restore -c Release

move /Y Panosen.Reflection\bin\Release\Panosen.Reflection.*.nupkg D:\LocalSavoryNuget\

pause