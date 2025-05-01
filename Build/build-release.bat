@echo off
set CONFIGURATION=Release
set SOLUTION=../Infra-CSharp.sln

echo Building solution with configuration: %CONFIGURATION%
@REM msbuild %SOLUTION% /p:Configuration=%CONFIGURATION%
dotnet build %SOLUTION% /p:Configuration=%CONFIGURATION%

echo Build completed!
pause