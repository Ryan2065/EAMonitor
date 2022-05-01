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
            $returnHash[$key] = $settingsHash[$key]
        }
    }

    
    if([string]::IsNullOrEmpty($Environment)){
        return $returnHash
    }

    $EnvironmentSettingsFile = [System.IO.Path]::Combine($Directory, "$($MonitorName).$($Environment).psd1")
    if(Test-Path $EnvironmentSettingsFile -ErrorAction SilentlyContinue){
        $envSettingsHash = Import-PowerShellDataFile -Path $EnvironmentSettingsFile
        foreach($key in $envSettingsHash.Keys){
            $returnHash[$key] = $envSettingsHash[$key]
        }
    }
    return $returnHash
}