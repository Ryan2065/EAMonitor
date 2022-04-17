Function Start-EAMonitor {
    Param(
        [Parameter(Mandatory = $false)]
        [string]$Name,
        [switch]$SkipSchedule
    )
    $EAMonitorParam = @{}
    if(-not [string]::IsNullOrWhiteSpace($Name)){
        $EAMonitorParam['Name'] = $Name
    }
    if($SkipSchedule.IsPresent){
        $EAMonitorParam['ScheduledToRunNow'] = $false
    }
    $MonitorsScheduledToRun = Get-EAMonitor @EAMonitorParam
    $MonitorsScheduledToRunNames = $MonitorsScheduledToRun.Name

    $RegisteredMonitorsToRun = New-Object System.Collections.Generic.List[RegisteredEAMonitor]

    foreach($monitor in $Script:RegisteredMonitors){
        if($MonitorsScheduledToRunNames -contains $monitor.Name){
            $RegisteredMonitorsToRun.Add($monitor)
        }
    }
}