. "$PSScriptRoot\buildModule.ps1"

$VerbosePreference = 'Continue'
$Global:DebugPreference = 'Continue'
$ErrorActionPreference = 'Stop'

Import-Module "$PSScriptRoot\EAMonitor" -Force -Verbose:$false -Scope 'Global'

$binPath = [System.IO.Path]::Combine($PSScriptRoot, "bin")
if(-not (Test-Path $binPath -ErrorAction SilentlyContinue)){
    $null = New-Item -ItemType Directory -Path $binPath -Force
}
foreach($file in Get-ChildItem $binPath -File){
    $null = Remove-Item $file.FullName -Force
}

$SqliteFile = [System.IO.Path]::Combine($binPath, 'EAMonitor.sqlite')

$env:PSModulePath = $env:PSModulePath + ";$($PSScriptRoot)"

Initialize-EAMonitor -SqliteFilePath $SqliteFile -CreateDb -DetectMonitorModules

Set-EAMonitorSetting -SettingKey 'WindowsServiceHealth-ComputerList' -Value 'Lab-CM.Home.Lab,Lab-DC.Home.Lab' -MonitorName 'Windows-ServiceHealth'

$results = Start-EAMonitor -SkipSchedule -PassThru
$results
