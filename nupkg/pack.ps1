. "./common.ps1"
$version=$args[0]
foreach($project in $projects) {
    $projectFolder = Join-Path $rootFolder $project
    Set-Location $projectFolder
    & dotnet restore
}

foreach($project in $projects) {
    $projectFolder = Join-Path $rootFolder $project
    # Create nuget pack
    Set-Location $projectFolder
    
    # 判断是否存在旧文件夹，如果存在则删除
    if (Test-Path (Join-Path $projectFolder "bin/Release")) {
        Remove-Item -Recurse (Join-Path $projectFolder "bin/Release")
    }
    & dotnet pack -c Release -p:PackageVersion=$version
}

Set-Location $packFolder