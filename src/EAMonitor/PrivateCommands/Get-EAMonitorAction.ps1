Function Get-EAMonitorAction{
    Param(
        [string]$Type,
        [string]$MonitorName
    )
    $Setting = (Get-EAMonitorSetting -MonitorName $MonitorName -Setting $Type).Value
    if([string]::IsNullOrEmpty($Setting)){
        $Setting = 'Default'
    }
    $Settings = @($Setting)
    if($Setting.Contains(',')){
        $Settings = @($Setting.Split(','))
    }
    foreach($action in [EAMonitor.Classes.EAMonitorModuleCache]::Actions){
        if($Settings -contains $action.Name -and $action.Type -eq $Type){
            return $action
        }
    }
}