function ZipFiles($zipfile, $sourcedir)
{
	[Environment]::CurrentDirectory = $PWD
    if (Test-Path $zipfile)
    {
        Remove-Item $zipfile
    } 
    Add-Type -Assembly System.IO.Compression.FileSystem
    $compressionLevel = [System.IO.Compression.CompressionLevel]::Optimal
    [System.IO.Compression.ZipFile]::CreateFromDirectory($sourcedir,
        $zipfile, $compressionLevel, $false)
}

function Unzip ($zipfile, $outputdir)
{
	[Environment]::CurrentDirectory = $PWD
	Add-Type -Assembly System.IO.Compression.FileSystem
    [System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $outputdir)
}
 