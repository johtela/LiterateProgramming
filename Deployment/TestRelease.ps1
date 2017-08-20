function Assert ([scriptblock] $condition)
{
    if (!(& $condition))
    {
        throw "Condition {$condition} does not hold."
    }
}

function TestPackage (
    [string] $scriptdir = $(throw "Specify script directory"),
	[string] $packagefile = $(throw "Zip package required"), 
	[string] $installdir = $(throw "Specify installation directory"))
{
	$scriptdir = Resolve-Path $scriptdir
	. $scriptdir\ZipHelpers.ps1
	$rootdir = Split-Path $scriptdir

    if (Test-Path $installdir)
    {
        Write-Host "Deleting old installation directory."
        Remove-Item $installdir -Recurse -Force
    }
    Write-Host "Installing the executables."
    Unzip $packagefile $installdir

    Write-Host "Generating HTML documentation for csweave."
    Push-Location
    Set-Location $rootdir
    [Environment]::CurrentDirectory = $rootdir
    & {
        $ErrorActionPreference = "SilentlyContinue"
        & $installdir\CSWeave.exe src\*.cs CSWeave.Theme\*.cs *.md -s LiterateProgramming.sln -o docs -f html -tv
    }
    Assert { $LASTEXITCODE -eq 0 }
    Assert { Test-Path docs }
    Assert { Test-Path docs\*.html }
    Assert { Test-Path docs\src\*.html }
    Assert { Test-Path docs\CSWeave.Theme\*.html }
    Assert { Test-Path docs\bootstrap }
    Assert { Test-Path docs\css\*.min.css }
    Assert { Test-Path docs\font-awesome }
    Assert { Test-Path docs\Images\*.png }
    Assert { Test-Path docs\mermaid\*.css }
    Assert { Test-Path docs\sidebar\*.min.css }
    Assert { Test-Path docs\syntax-highlight\*.min.css }
    Pop-Location
}