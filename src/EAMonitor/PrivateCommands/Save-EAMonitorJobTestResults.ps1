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
        if($null -eq $MonitorObject){
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

        $NewResult = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorJobTest
        $NewResult.Id = ( New-Guid ).Guid
        $NewResult.JobId = $JobObject.Id
        $NewResult.TestPath = ($result.Path -join '.')
        $NewResult.TestExpandedPath = $result.ExpandedPath 
        $NewResult.Passed = $result.Passed
        $NewResult.ExecutedAt = $result.ExecutedAt
        Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $NewResult
        $SaveCount++
        if($SaveCount -eq 30){
            $SaveCount = 0
            Save-EAMonitorContext
        }
    }
    foreach($container in $results.Containers){
        $MonitorFile = $container.Item
        $MonitorObject = $null
        foreach($mon in $Monitors){
            if($mon.FilePath -eq $MonitorFile){
                $MonitorObject = $mon
            }
        }
        if($null -eq $MonitorObject){
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
        $MonitorState = 'Down'
        if($true -eq $result.Passed){
            $JobStatus = 'Completed'
            $MonitorState = 'Up'
        }
        $MonitorStateObject = (Get-EAMonitorState -StateName $MonitorState)
        $JobStateObject = (Get-EAMonitorJobStatus -Name $JobStatus)
        $JobObject.JobStatusId = $JobStateObject.Id
        $JobObject.MonitorStateId = $MonitorStateObject.Id
        $MonitorObject.DbMonitorObject.MonitorStateId = $MonitorStateObject.Id

    }
    Save-EAMonitorContext
}