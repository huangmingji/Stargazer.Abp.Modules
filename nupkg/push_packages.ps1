. ".\common.ps1"

$version = $args[0]
$nugetApiKey = $args[1]
$githubApiKey = $args[2]

# Get the version
#[xml]$commonPropsXml = Get-Content (Join-Path $rootFolder "common.props")
#$version = $commonPropsXml.Project.PropertyGroup.Version

# Publish all packages
foreach($project in $projects) {
    $projectName = $project.Substring($project.LastIndexOf("/") + 1)
    $projectFolder = Join-Path $rootFolder $project
    # Create nuget pack
    Set-Location (Join-Path $projectFolder "bin/Release")
    & dotnet nuget push ($projectName + "." + $version + "*.nupkg") --skip-duplicate --source "https://api.nuget.org/v3/index.json" --api-key $nugetApiKey
    & dotnet nuget push ($projectName + "." + $version + "*.nupkg") --skip-duplicate --source "https://nuget.pkg.github.com/huangmingji/index.json" --api-key $githubApiKey
}

# Go back to the pack folder
Set-Location $packFolder