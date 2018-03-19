$ErrorActionPreference = "Stop"
$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

. $scriptPath/BuildRelease.ps1
. $scriptPath/TestRelease.ps1

$zipfile = Package $scriptPath
TestPackage $scriptPath $zipfile $env:LOCALAPPDATA\csweave