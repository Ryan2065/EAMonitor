
Class RegisteredEAMonitor{
    [string]$Name
    [string]$FilePath
    [string]$Directory
    [object]$DbMonitorObject
    RegisteredEAMonitor([string]$Path){
        
        $this.FilePath = $path
        $FileObj = Get-Item $Path
        $monName = $FileObj.BaseName -replace '.monitors',''
        $this.Name = $monName
        $this.Directory = $FileObj.Directory.FullName
        $localSettings = $this.GetLocalSettings()
        $Description = $localSettings['Description']
        if(-not [string]::IsNullOrEmpty($monName)){
            $this.DbMonitorObject = Register-EAMonitor -MonitorName $monName -Description $Description
        }
        else{
            Write-Warning "Could not find a name for monitor $($path)"
        }
        
    }
    [HashTable]GetLocalSettings(){
        $EAMonEnv = $Script:EAMonitorEnvironment
        return $this.GetLocalSettings($EAMonEnv)
    }
    [HashTable]GetLocalSettings([string]$EAMonEnv){
        $returnHash = @{}
        
        $DefaultSettingsFile = [System.IO.Path]::Combine($this.Directory, "$($this.Name).psd1")
        if(Test-Path $DefaultSettingsFile -ErrorAction SilentlyContinue){
            $settingsHash = Import-PowerShellDataFile -Path $DefaultSettingsFile
            foreach($key in $settingsHash.Keys){
                $returnHash[$key] = $settingsHash[$key]
            }
        }

        
        if([string]::IsNullOrEmpty($EAMonEnv)){
            return $returnHash
        }

        $EnvironmentSettingsFile = [System.IO.Path]::Combine($this.Directory, "$($this.Name).$($EAMonEnv).psd1")
        if(Test-Path $EnvironmentSettingsFile -ErrorAction SilentlyContinue){
            $envSettingsHash = Import-PowerShellDataFile -Path $EnvironmentSettingsFile
            foreach($key in $envSettingsHash.Keys){
                $returnHash[$key] = $envSettingsHash[$key]
            }
        }
        return $returnHash
    }
}