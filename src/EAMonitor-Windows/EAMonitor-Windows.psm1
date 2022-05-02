Foreach($MonitorFile in (Get-ChildItem -Path "$PSScriptRoot\Monitors" -Filter '*.monitors.ps1' -ErrorAction SilentlyContinue)){
    Import-EAMonitor -Path $MonitorFile.FullName
}

$Commands = Get-ChildItem -Path "$PSScriptRoot\Commands" -Filter '*.ps1' -ErrorAction SilentlyContinue
Foreach($c in $Commands){
    . $c.FullName
}

Export-ModuleMember -Function $Commands.BaseName