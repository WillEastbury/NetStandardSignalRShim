$preftarget = "netstandard2.1"
$alttarget = "netstandard2.0"

$outDir = ".\out"
$pluginDir = ".\out\bin"

nuget install Microsoft.AspNetCore.SignalR.Client -OutputDirectory $outdir

$packages = Get-ChildItem -Path $outDir
foreach ($p in $packages) {
    Write-Output "Testing $($p.FullName)"
	$dll = Get-ChildItem -Path "$($p.FullName)\lib\$($preftarget)\*.dll" -ErrorAction:Ignore
	if (!($null -eq $dll)) {
		$d = $dll[0]
		if (!(Test-Path "$($pluginDir)\$($d.Name)")) {
            Write-Output "Copying $preftarget -> $($pluginDir)\$($d.Name)"
			Move-Item -Path $d.FullName -Destination $pluginDir
		}
	}
    else 
    {
        $temp = Get-ChildItem -Path "$($p.FullName)\lib\$($alttarget)\*.dll" -ErrorAction:Ignore
        if (!($null -eq $temp)) {
            $d = $temp[0]
            if (!(Test-Path "$($pluginDir)\$($d.Name)")) {
                Write-Output "Copying $alttarget -> $($pluginDir)\$($d.Name)"
                Move-Item -Path $d.FullName -Destination $pluginDir
            }
        }
    }
}
