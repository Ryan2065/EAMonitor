Foreach($MonitorFile in (Get-ChildItem -Path "$PSScriptRoot\Monitors" -Filter '*.tests.ps1' -ErrorAction SilentlyContinue)){
    Import-EAMonitor -Path $MonitorFile.FullName
}

