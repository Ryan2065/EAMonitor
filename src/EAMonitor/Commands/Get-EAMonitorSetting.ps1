Function Get-EAMonitorSetting{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$MonitorName,
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$SettingKey,
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$Environment
    )
    $EnvironmentObject = $null

    if($PSBoundParameters.ContainsKey('Environment')){
        if(-not ([string]::IsNullOrEmpty($Environment))){
            $EnvironmentObject = Search-EFPosh -Entity $Script:EAMonitorDbContext.EAMonitorEnvironment -Expression { $_.Name -eq $0 } -Arguments @(,$Environment) -FirstOrDefault
        }
    }
    else{
        $EnvironmentObject = Get-EAMonitorEnvironment
    }

    $SearchArguments = @{
        'ToList' = $true
        'Entity' = $Script:EAMonitorDbContext.EAMonitorSetting
        'Include' = @('SettingKey')
    }

    $DefaultEAMonitorSettings = Search-EFPosh -Expression { $_.MonitorId -eq $0 -and $_.SettingEnvironmentId -eq $0 } -Arguments @(,$null) @SearchArguments
    $DefaultEAMonitorEnvironmentSettings = $null
    if($null -ne $EnvironmentObject){
        $DefaultEAMonitorEnvironmentSettings = Search-EFPosh -Expression { $_.MonitorId -eq $0 -and $_.SettingEnvironmentId -eq $1 } -Arguments @(,$null, $EnvironmentObject.Id) @SearchArguments
    }
    $SpecificMonitorSettings = $null
    if(-not [string]::IsNullOrEmpty($MonitorName)){
        $SpecificMonitorSettings = Search-EFPosh -Expression { $_.Monitor.Name -eq $0 -and $_.SettingEnvironmentId -eq $1 } -Arguments @(,$MonitorName,$null) @SearchArguments
    }
    $SpecificMonitorEnvironmentSettings = $null
    if($null -ne $EnvironmentObject -and (-not [string]::IsNullOrEmpty($MonitorName))){
        $SpecificMonitorEnvironmentSettings = Search-EFPosh -Expression { $_.Monitor.Name -eq $0 -and $_.SettingEnvironmentId -eq $1 } -Arguments @(,$MonitorName,$EnvironmentObject.Id) @SearchArguments
    }
    $MonitorLocalSettings = @{}
    if($null -ne $Script:ImportedMonitors -and ( -not [string]::IsNullOrEmpty($MonitorName ))){
        foreach($mon in $Script:ImportedMonitors){
            if($mon.Name -eq $MonitorName){
                $MonitorLocalSettings = $mon.GetLocalSettings()
            }
        }
    }

    #now compile them all in order of least importance
    $returnHash = @{}
    foreach($setting in $DefaultEAMonitorSettings){
        $returnHash[$setting.SettingKey.Name] = $setting.SettingValue
    }
    foreach($setting in $DefaultEAMonitorEnvironmentSettings){
        $returnHash[$setting.SettingKey.Name] = $setting.SettingValue
    }
    foreach($key in $MonitorLocalSettings.Keys){
        $returnHash[$key] = $MonitorLocalSettings[$key]
    }
    foreach($setting in $SpecificMonitorSettings){
        $returnHash[$setting.SettingKey.Name] = $setting.SettingValue
    }
    foreach($setting in $SpecificMonitorEnvironmentSettings){
        $returnHash[$setting.SettingKey.Name] = $setting.SettingValue
    }
    return $returnHash
}