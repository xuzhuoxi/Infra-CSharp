param(
    [string]$configuration = "Release"
)

$solutionFile = "Infra-CSharp.sln"

Write-Host "Building solution with configuration: $configuration"
Start-Process -NoNewWindow -Wait -FilePath "msbuild.exe" -ArgumentList "$solutionFile /p:Configuration=$configuration"

Write-Host "Build completed!"

# 如果用户传入 -publish 选项，则执行发布
if ($publish) {
    Write-Host "Publishing solution..."
    Start-Process -NoNewWindow -Wait -FilePath "dotnet" -ArgumentList "publish $solutionFile --configuration $configuration --output ./publish"
    Write-Host "Publish completed!"
}