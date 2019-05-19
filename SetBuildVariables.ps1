# This file sets build variables to be used in Azure Pipelines

if ($Env:BUILD_SOURCEBRANCH -match "^refs\/tags\/v(\d[\.0-9]*)") 
{
    $version = $matches[1]
    $packageVersion = $version
}
else
{
    $version = "$Env:BUILD_BUILDNUMBER"
    $packageVersion = "$version-ci"
}

Write-Output "Setting build variables..."
Write-Output "version=$version"
Write-Output "packageVersion=$packageVersion"
Write-Output "##vso[task.setvariable variable=version]$version"
Write-Output "##vso[task.setvariable variable=packageVersion]$packageVersion"