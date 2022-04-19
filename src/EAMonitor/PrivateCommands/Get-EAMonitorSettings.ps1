Function Get-EAMonitorSettings{
    Param(
        [RegisteredEAMonitor]$Monitor
    )
    $MonitorSettings = @{}

    Get-EAMonitorDefaultSettings
    
    $MonitorSettingsFile = [System.IO.Path]::Combine($Monitor.Directory, "$($Monitor.Name).psd1")
    if(-not ( [string]::IsNullOrWhiteSpace((Get-EAMonitorEnvironment)))){
        $MonitorEnvironmentSettingsFile = [System.IO.Path]::Combine($Monitor.Directory, "$($Monitor.Name).$(Get-EAMonitorEnvironment).psd1")
    }
}