#. "$PSScriptRoot\buildModule.ps1"


$VerbosePreference = 'Continue'
$Global:DebugPreference = 'Continue'
$ErrorActionPreference = 'Stop'

Import-Module "$PSScriptRoot\EAMonitor" -Force -Verbose:$false 

$binPath = [System.IO.Path]::Combine($PSScriptRoot, "bin")
if(-not (Test-Path $binPath -ErrorAction SilentlyContinue)){
    $null = New-Item -ItemType Directory -Path $binPath -Force
}
foreach($file in Get-ChildItem $binPath -File){
    $null = Remove-Item $file.FullName -Force
}

$SqliteFile = [System.IO.Path]::Combine($binPath, 'EAMonitor.sqlite')

$env:PSModulePath = $env:PSModulePath + ";$($PSScriptRoot)\ExampleMonitors"

Initialize-EAMonitor -SqliteFilePath $SqliteFile -CreateDb -DetectMonitorModules

Set-EAMonitorSetting -Key 'WindowsServiceHealth-ComputerList' -Value 'Lab-CM.Home.Lab,Lab-DC.Home.Lab' -MonitorName 'Windows-ServiceHealth'
Set-EAMonitorSetting -Key 'SendMailFrom' -Value 'Monitor@EphingAdmin.com'
Set-EAMonitorSetting -Key 'SendMailTo' -Value 'Ryan@Eph-It.com' 
Set-EAMonitorSetting -Key 'SendMailSmtp' -Value 'smtp-relay.sendinblue.com'
Set-EAMonitorSetting -Key 'SendMailSmtpPort' -Value '587'
Set-EAMonitorSetting -Key 'SendMailEnableSSl' -Value $true
Set-EAMonitorSetting -Key 'SendMailCredentials' -Value 'EaMonitorCredentials'
Set-EAMonitorSetting -Key 'RepeatMinuteInterval' -Value 15
Set-EAMonitorSetting -Key 'Enabled' -Value $true
Set-EAMonitorSetting -Key 'Enabled' -Value $false -MonitorName 'Windows-ServiceHealth'
#$Credential = New-Object System.Management.Automation.PSCredential('ryan2065@gmail.com', (ConvertTo-SecureString $env:smtpkey -AsPlainText -Force) )
#Set-Secret -Name 'EaMonitorCredentials' -Secret $Credential

$results = Start-EAMonitor -Passthru 
$results