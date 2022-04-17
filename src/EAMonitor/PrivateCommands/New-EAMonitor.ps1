Function New-EAMonitor {
    Param(
        [string]$Name,
        [string]$Path
    )

    $newEAMonitorObj = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitor
    $newEAMonitorObj.Id = ( New-Guid ).Guid
    $newEAMonitorObj.Name = $Name
    $CurrentTime = [DateTime]::UtcNow
    $newEAMonitorObj.LastModified = $CurrentTime
    $newEAMonitorObj.Created = $CurrentTime

    $newEAMonitorObj.MonitorStateId = (Get-EAMonitorState -StateName 'Unknown').Id
    
    $MonitorSettings = Get-EAMonitorLocalSettings -MonitorName $Name -Path $Path
    if($null -ne $MonitorSettings){
        $newEAMonitorObj.Description = $MonitorSettings.Description
    }
    
    $newEAMonitorObj.NextRun = [DateTime]::UtcNow

    Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $newEAMonitorObj -SaveChanges
}