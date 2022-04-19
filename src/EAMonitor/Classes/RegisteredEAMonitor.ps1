
Class RegisteredEAMonitor{
    [string]$Name
    [string]$FilePath
    [string]$Directory
    [string]$Module
    [object]$DbMonitorObject
    [HashTable]$LocalSettings
    [Hashtable]$LocalEnvironmentSettings
    RegisteredEAMonitor([string]$Path, [string]$FromModule){
        $this.FilePath = $path
        $FileObj = Get-Item $Path
        $this.Name = $FileObj.BaseName -replace '.tests.ps1',''
        $this.Directory = $FileObj.Directory.FullName
        $this.Module = $FromModule
    }
    [HashTable]GetLocalSettings(){
        $returnHash = @{}
        
        $DefaultSettingsFile = [System.IO.Path]::Combine($this.Directory, "$($this.Name).psd1")
        if(Test-Path $DefaultSettingsFile -ErrorAction SilentlyContinue){
            $settingsHash = Import-PowerShellDataFile -Path $DefaultSettingsFile
            foreach($key in $settingsHash.Keys){
                $returnHash[$key] = $settingsHash[$key]
            }
        }

        $EAMonEnv = Get-EAMonitorEnvironment
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