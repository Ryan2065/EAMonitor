Function Save-EAMonitorJobTestResults {
    Param(
        [EAMonitor.Classes.EAMonitorResult[]]$results
    )
    $SaveCount = 0
    $MonitorResults = @{}
    foreach($result in $results){
        $MonitorObject = $result.Monitor
        $JobObject = $result.Job
        $testResult = $result.TestResult

        $NewResult = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorJobTest
        $NewResult.Id = ( New-Guid ).Guid
        $NewResult.JobId = $JobObject.Id
        $NewResult.TestPath = ($testResult.Path -join '.')
        $NewResult.TestExpandedPath = $testResult.ExpandedPath 
        $NewResult.Passed = $testResult.Passed
        $NewResult.ExecutedAt = $testResult.ExecutedAt
        if($null -ne $result.Data){
            $NewResult.Data = $result.Data | ConvertTo-JSON -Compress
        }
        Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $NewResult
        $SaveCount++
        if($SaveCount -eq 30){
            $SaveCount = 0
            Save-EAMonitorContext
        }
        if(-not ($MonitorResults.ContainsKey($MonitorObject.DbMonitorObject.Id))){
            $MonitorResults[$MonitorObject.DbMonitorObject.Id] = $result
        }
        if($false -eq $testResult.Passed){
            $MonitorResults[$MonitorObject.DbMonitorObject.Id] = $result
        }
    }
    Save-EAMonitorContext
    foreach($key in $MonitorResults.Keys){
        $MonitorResult = $MonitorResults[$key]
        $MonitorObject = $MonitorResult.Monitor
        $JobObject = $MonitorResult.Job
        $testResult = $MonitorResult.TestResult
        $JobStatus = 'Failed'
        $MonitorState = 'Down'
        if($true -eq $testResult.Passed){
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