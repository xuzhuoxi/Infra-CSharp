param(
    [string]$configuration = "Release"
)

$solutionFile = "Infra-CSharp.sln"

Write-Host "Building solution with configuration: $configuration"
Start-Process -NoNewWindow -Wait -FilePath "msbuild.exe" -ArgumentList "$solutionFile /p:Configuration=$configuration"

Write-Host "Build completed!"
