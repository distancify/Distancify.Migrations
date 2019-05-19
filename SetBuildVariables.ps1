# This file sets build variables to be used in Azure Pipelines

if ($Env:BUILD_SOURCEBRANCH -match "^refs\/tags\/v(\d[\.0-9]*)") 
{
    $fileVersion = $matches[1]
    $packageVersion = $fileVersion
}
else
{
    $fileVersion = "$Env:BUILD_BUILDNUMBER"
    $packageVersion = "$fileVersion-ci"
}

Write-Output "Setting build variables..."
Write-Output "fileVersion=$fileVersion"
Write-Output "packageVersion=$packageVersion"
Write-Output "##vso[task.setvariable variable=fileVersion]$version"
Write-Output "##vso[task.setvariable variable=packageVersion]$packageVersion"