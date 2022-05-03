Function Get-EAMonitorLocalSettings{
    Param(
        [string]$MonitorName,
        [string]$Directory,
        [string]$Environment
    )
    $returnHash = @{}
        
    $DefaultSettingsFile = [System.IO.Path]::Combine($Directory, "$($MonitorName).psd1")
    if(Test-Path $DefaultSettingsFile -ErrorAction SilentlyContinue){
        $settingsHash = Import-PowerShellDataFile -Path $DefaultSettingsFile
        foreach($key in $settingsHash.Keys){
            $returnHash[$key] = [EAMonitor.Classes.EAMonitorSettingObject]::new($key, $settingsHash[$key], 'MonitorFile', $DefaultSettingsFile )
        }
    }

    
    if([string]::IsNullOrEmpty($Environment)){
        foreach($key in $returnHash.Keys){
            $returnHash[$key]
        }
        return
    }

    $EnvironmentSettingsFile = [System.IO.Path]::Combine($Directory, "$($MonitorName).$($Environment).psd1")
    if(Test-Path $EnvironmentSettingsFile -ErrorAction SilentlyContinue){
        $envSettingsHash = Import-PowerShellDataFile -Path $EnvironmentSettingsFile
        foreach($key in $envSettingsHash.Keys){
            $returnHash[$key] =  [EAMonitor.Classes.EAMonitorSettingObject]::new($key, $envSettingsHash[$key], 'MonitorEnvironmentFile', $EnvironmentSettingsFile)
        }
    }
    foreach($key in $returnHash.Keys){
        $returnHash[$key]
    }
}