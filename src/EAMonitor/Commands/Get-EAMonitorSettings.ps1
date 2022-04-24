Function Get-EAMonitorSettings{
    [CmdletBinding()]
    param(
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$MonitorName,
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$Setting,
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

    # Cache settings so we don't hit the database every time
    $eaMonitorDbSettings = Get-EAMonitorCachedData -Name 'Settings' -ActiveFor ( New-TimeSpan -Minutes 10 ) -ScriptBlock {
        Write-Debug "Retrieving all settings from DB"
        $SearchArguments = @{
            'ToList' = $true
            'Entity' = $Script:EAMonitorDbContext.EAMonitorSetting
            'Include' = @('SettingKey', 'Monitor')
        }
        Search-EFPosh @SearchArguments
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
    foreach($key in $MonitorLocalSettings.Keys){
        $returnHash[$key] = $MonitorLocalSettings[$key]
    }
    #Default settings from database - where no environment and no monitor listed
    $eaMonitorDbSettings | Where-Object { $null -eq $_.SettingEnvironmentId -and $null -eq $_.MonitorId } | ForEach-Object{
        $returnHash[$_.SettingKey.Name] = $_.SettingValue
    }
    #default environment settings - environment specified but no monitor listed
    if($null -ne $EnvironmentObject){
        $eaMonitorDbSettings | Where-Object { $EnvironmentObject.Id -eq $_.SettingEnvironmentId -and $null -eq $_.MonitorId } | ForEach-Object{
            $returnHash[$_.SettingKey.Name] = $_.SettingValue
        }
    }
    # Monitor specific settings
    if(-not [string]::IsNullOrEmpty($MonitorName)){
        #Monitor specific settings - no environment specified
        $eaMonitorDbSettings | Where-Object { $null -eq $_.SettingEnvironmentId -and $_.Monitor.Name -eq $MonitorName } | ForEach-Object{
            $returnHash[$_.SettingKey.Name] = $_.SettingValue
        }
        #Monitor specific environment settings
        if($null -ne $EnvironmentObject){
            $eaMonitorDbSettings | Where-Object { $EnvironmentObject.Id -eq $_.SettingEnvironmentId -and $_.Monitor.Name -eq $MonitorName } | ForEach-Object{
                $returnHash[$_.SettingKey.Name] = $_.SettingValue
            }
        }
    }
    foreach($setting in $SpecificMonitorSettings){
        $returnHash[$setting.SettingKey.Name] = $setting.SettingValue
    }
    foreach($setting in $SpecificMonitorEnvironmentSettings){
        $returnHash[$setting.SettingKey.Name] = $setting.SettingValue
    }
    if($Setting){
        return $returnHash."$Setting"
    }
    else{
        return $returnHash
    }
}