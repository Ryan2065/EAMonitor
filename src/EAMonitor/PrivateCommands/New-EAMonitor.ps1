Function New-EAMonitor {
    Param(
        [string]$Name,
        [string]$Description
    )

    $newEAMonitorObj = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitor
    $newEAMonitorObj.Id = ( New-Guid ).Guid
    $newEAMonitorObj.Name = $Name
    $CurrentTime = [DateTime]::UtcNow
    $newEAMonitorObj.LastModified = $CurrentTime
    $newEAMonitorObj.Created = $CurrentTime

    $newEAMonitorObj.MonitorStateId = (Get-EAMonitorState -StateName 'Unknown').Id
    $newEAMonitorObj.Description = $Description

    Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $newEAMonitorObj

    Save-EAMonitorContext
}

