Function Get-EAMonitorLocalSettings{
    Param(
        $MonitorName,
        $Path
    )
    $SettingsDirectory = ( Get-Item $Path ).Directory.FullName
    $SettingsFile = [System.IO.Path]::Combine($SettingsDirectory, "$($MonitorName).psd1")
    if(Test-Path $SettingsFile -ErrorAction SilentlyContinue){
        return Import-PowerShellDataFile -Path $SettingsFile
    }

}