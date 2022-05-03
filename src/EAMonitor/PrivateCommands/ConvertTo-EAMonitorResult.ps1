Function ConvertTo-EAMonitorResult{
    Param(
        [Pester.Run]$results, [EAMonitor.Classes.EAMonitorRegistered[]]$Monitors, [EAMonitor.EAMonitorJob[]]$Jobs
    )
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
        $newResult = New-Object EAMonitor.Classes.EAMonitorResult
        $newResult.Monitor = $MonitorObject
        $newResult.Job = $JobObject
        $newResult.TestResult = $result
        
        $ProcessTestDataAction = Get-EAMonitorAction -MonitorName $newResult.Monitor.Name -Type 'ProcessTestData'
        try{
            $composeDataResult = Invoke-Command -ScriptBlock $ProcessTestDataAction.Script -ArgumentList $result
        }
        catch{
            Write-Error "Error processing results for Monitor $($newResult.Monitor.Name) with action $($ProcessTestDataAction.Name)" -Exception $_ -ErrorAction Continue
            continue
        }
        if($null -ne $composeDataResult){
            $newResult.Data = $composeDataResult
        }
        else{
            $newResult.Data = [PSCustomObject]@{
                Test = $result.ExpandedPath
                Passed = $result.Passed
            }
        }

        $newResult
    }
}