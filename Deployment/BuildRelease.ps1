function Cleanup ($path)
{
	if (Test-Path $path)
	{
		Remove-Item $path -Recurse -Force
	}
}

function Package ([string] $scriptdir = $(throw "Script directory required."))
{
	$scriptdir = Resolve-Path $scriptdir
	. $scriptdir\ZipHelpers.ps1

	$rootdir = Split-Path $scriptdir
	Write-Host "Project directory: $rootdir"
	$exefile = "$rootdir\bin\Release\CSWeave.exe"
	$vi = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($exefile)
    $major = $vi.FileMajorPart
    $minor = $vi.FileMinorPart
    $build = $vi.FileBuildPart
    $version = "$major.$minor.$build"
	Write-Host "csweave version: $version"

	Write-Host "Cleaning up temporary directory."
	Cleanup $scriptdir\Install
	Write-Host "Copying release files."
	Copy-Item $rootdir\bin\Release\ -Destination $scriptdir\Install\ -Recurse -Container -Exclude *.pdb,*.xml,*vshost*
	Write-Host "Zipping files."
	$result = "$scriptdir\csweave_release_$version.zip"
	ZipFiles $result $scriptdir\Install
	Write-Host "Cleaning up temporary directory."
	Cleanup $scriptdir\Install
	$result
}