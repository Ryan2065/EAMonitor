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
        [string]$Environment,
        [switch]$AsHashtable
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
    foreach($localSetting in $MonitorLocalSettings){
        $returnHash[$localSetting.Key] = $localSetting
    }
    #Default settings from database - where no environment and no monitor listed
    $eaMonitorDbSettings | Where-Object { $null -eq $_.MonitorId } | ForEach-Object{
        $returnHash[$_.SettingKey.Name] = [EAMonitor.Classes.EAMonitorSettingObject]::new($_.SettingKey.Name, $_.SettingValue, 'SqlDefault', $_.Id, $_.SettingKey.Description)
    }
    # Monitor specific settings
    if(-not [string]::IsNullOrEmpty($MonitorName)){
        #Monitor specific settings - no environment specified
        $eaMonitorDbSettings | Where-Object { $_.Monitor.Name -eq $MonitorName } | ForEach-Object{
            $returnHash[$_.SettingKey.Name] = [EAMonitor.Classes.EAMonitorSettingObject]::new($_.SettingKey.Name, $_.SettingValue, 'SqlMonitor', $_.Id, $_.SettingKey.Description, $MonitorName)
        }
    }
    
    if($Setting){
        return $returnHash."$Setting"
    }
    else{
        if($AsHashtable){
            $newReturnHash = @{}
            foreach($key in $returnHash.Keys){
                $newReturnHash[$key] = $returnHash[$key].Value
            }
            return $newReturnHash
        }
        else{
            foreach($key in $returnHash.Keys){
                $returnHash[$key]
            }
        }
        
    }
}