@echo off
set CONFIGURATION=Debug
set SOLUTION=../Infra-CSharp.sln

echo Publishing solution with configuration: %CONFIGURATION%
@REM msbuild %SOLUTION% /p:Configuration=%CONFIGURATION%
dotnet publish %SOLUTION% /p:Configuration=%CONFIGURATION%

echo publish completed!
pause