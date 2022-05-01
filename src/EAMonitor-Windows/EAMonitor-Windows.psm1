Foreach($MonitorFile in (Get-ChildItem -Path "$PSScriptRoot\Monitors" -Filter '*.monitors.ps1' -ErrorAction SilentlyContinue)){
    Import-EAMonitor -Path $MonitorFile.FullName
}

