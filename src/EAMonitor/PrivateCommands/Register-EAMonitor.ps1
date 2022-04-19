Function Register-EAMonitor{
    [CmdletBinding()]
    param(
        [string]$Name,
        [string]$Description
    )
    $MonitorObject = Get-EAMonitor -Name $MonitorName
    if($null -eq $MonitorObject){
        New-EAMonitor -Name $MonitorName -Description $Description
        $MonitorObject = Get-EAMonitor -Name $MonitorName
    }
    if($null -eq $MonitorObject){
        throw "Could not find or create monitor $($MonitorName)"
        return
    }

    if(-not [string]::IsNullOrEmpty($Description)){
        if($MonitorObject.Description -ne $Description){
            $MonitorObject.Description = $Description
            $MonitorObject.LastModified = [datetime]::UtcNow
            Save-EAMonitorContext
        }
    }

    return $MonitorObject
}