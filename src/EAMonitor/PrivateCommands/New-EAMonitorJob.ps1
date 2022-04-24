Function New-EAMonitorJob{
    Param(
        [RegisteredEAMonitor[]]$Monitors
    )
    $Jobs = @()
    foreach($monitor in $monitors){
        $newEAJobObj = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorJob
        $newEAJobObj.Id = ( New-Guid ).Guid
        $newEAJobObj.MonitorId = $monitor.DbMonitorObject.Id
        $newEAJobObj.JobStatusId = (Get-EAMonitorJobStatus -Name 'Created').Id
        $CurrentTime = [DateTime]::UtcNow
        $newEAJobObj.LastModified = $CurrentTime
        $newEAJobObj.Created = $CurrentTime
    
        $newEAJobObj.Notified = $false
        $newEAJobObj.MonitorStateId = (Get-EAMonitorState -Name 'Unknown').Id
    
        Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $newEAJobObj
        
        $Jobs += $newEAJobObj
    }
    Save-EAMonitorContext
    return $Jobs
}