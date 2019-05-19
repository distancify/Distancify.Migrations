# This file sets the build variable "version" to be used in Azure Pipelines

if ($Env:BUILD_SOURCEBRANCH -match "^refs\/tags\/v(\d[\.0-9]*)") 
{
    $version = $matches[1]
}
else
{
    $version = "$Env:BUILD_BUILDNUMBER-ci"
}

Write-Output "##vso[task.setvariable variable=version]$version"