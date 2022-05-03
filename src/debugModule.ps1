. "$PSScriptRoot\buildModule.ps1"

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

$env:PSModulePath = $env:PSModulePath + ";$($PSScriptRoot)"

Initialize-EAMonitor -SqliteFilePath $SqliteFile -CreateDb -DetectMonitorModules

Set-EAMonitorSetting -SettingKey 'WindowsServiceHealth-ComputerList' -Value 'Lab-CM.Home.Lab,Lab-DC.Home.Lab' -MonitorName 'Windows-ServiceHealth'
Set-EAMonitorSetting -SettingKey 'SendMailFrom' -Value 'Monitor@EphingAdmin.com'
Set-EAMonitorSetting -SettingKey 'SendMailTo' -Value 'Ryan@Eph-It.com' 
Set-EAMonitorSetting -SettingKey 'SendMailSmtp' -Value 'smtp-relay.sendinblue.com'
Set-EAMonitorSetting -SettingKey 'SendMailSmtpPort' -Value '587'
Set-EAMonitorSetting -SettingKey 'SendMailEnableSSl' -Value $true
Set-EAMonitorSetting -SettingKey 'SendMailCredentials' -Value 'EaMonitorCredentials'
#$Credential = New-Object System.Management.Automation.PSCredential('ryan2065@gmail.com', (ConvertTo-SecureString $env:smtpkey -AsPlainText -Force) )
#Set-Secret -Name 'EaMonitorCredentials' -Secret $Credential

$results = Start-EAMonitor -SkipSchedule -PassThru
$results
