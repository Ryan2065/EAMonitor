Function Get-EAMonitorSetting{
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
    $Env = $Script:EAMonitorEnvironment
    if(-not ([string]::IsNullOrEmpty($Environment))){
        $env = $Environment
    }
    # Cache settings so we don't hit the database every time
    $eaMonitorDbSettings = Get-EAMemoryCacheValue -Key 'Settings' -ActiveFor ( New-TimeSpan -Minutes 10 ) -Action {
        Write-Debug "Retrieving all settings from DB"
        $SearchArguments = @{
            'ToList' = $true
            'DbContext' = $Script:EAMonitorDbContext
            'Entity' = 'EAMonitorSetting'
            'Include' = @('SettingKey', 'Monitor')
        }
        Search-EFPosh @SearchArguments
    }
    
    $MonitorLocalSettings = @{}
    if($null -ne $Script:ImportedMonitors -and ( -not [string]::IsNullOrEmpty($MonitorName ))){
        foreach($mon in $Script:ImportedMonitors){
            if($mon.Name -eq $MonitorName){
                $MonitorLocalSettings = Get-EAMonitorLocalSettings -MonitorName $mon.Name -Directory $mon.Directory -Environment $env
            }
        }
    }
    
    #now compile them all in order of least importance: Settings bunbled with the monitor -> Db general settings -> Db settings for this monitor
    $returnHash = @{}
    foreach($key in $MonitorLocalSettings.Keys){
        $returnHash[$key] = $MonitorLocalSettings[$key]
    }
    #Default settings from database - where no environment and no monitor listed
    $eaMonitorDbSettings | Where-Object { $null -eq $_.MonitorId } | ForEach-Object{
        $returnHash[$_.SettingKey.Name] = $_.SettingValue
    }
    # Monitor specific settings
    if(-not [string]::IsNullOrEmpty($MonitorName)){
        #Monitor specific settings - no environment specified
        $eaMonitorDbSettings | Where-Object { $_.Monitor.Name -eq $MonitorName } | ForEach-Object{
            $returnHash[$_.SettingKey.Name] = $_.SettingValue
        }
    }

    if($Setting){
        return $returnHash."$Setting"
    }
    else{
        return $returnHash
    }
}