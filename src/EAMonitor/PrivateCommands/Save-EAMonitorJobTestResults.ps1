Function Save-EAMonitorJobTestResults {
    Param(
        [object[]]$results, [RegisteredEAMonitor[]]$Monitors, [object[]]$Jobs
    )
    $SaveCount = 0
    foreach($result in $results.Tests){
        $MonitorFile = $result.Block.BlockContainer.Item
        $MonitorObject = $null
        foreach($mon in $Monitors){
            if($mon.FilePath -eq $MonitorFile){
                $MonitorObject = $mon
            }
        }
        if($null -eq $monitorId){
            Write-Warning "Could not parse results for test file $($MonitorFile)"
            continue
        }
        $JobObject = $null
        foreach($job in $jobs){
            if($job.MonitorId -eq $MonitorObject.DbMonitorObject.Id){
                $JobObject = $job
            }
        }
        if($null -eq $JobObject){
            Write-Warning "Could not parse results for test file $($MonitorFile)"
            continue
        }
        $JobStatus = 'Failed'
        $MonitorState = 'Up'
        if($true -eq $result.Passed){
            $JobStatus = 'Completed'
            $MonitorState = 'Down'
        }
        $JobObject.JobStautsId = (Get-EAMonitorJobStatus -Name $JobStatus).Id
        $MonitorObject.DbMonitorObject.MonitorStateId = (Get-EAMonitorState -StateName $MonitorState).Id
        $NewResult = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorJobTest
        $NewResult.Id = ( New-Guid ).Guid
        $NewResult.JobId = $JobObject.Id
        $NewResult.TestPath = ($result.Path -join '.')
        $NewResult.TestExpandedPath = $result.ExpandedPath 
        $NewResult.Passed = $result.Passed
        $NewResult.ExecutedAt = $result.ExecutedAt
        $SaveCount++
        if($SaveCount -eq 30){
            $SaveCount = 0
            Save-EAMonitorContext
        }
    }
    Save-EAMonitorContext
}