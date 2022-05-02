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
        try{
            $ComposeDataSb = Get-EAMonitorSetting -Setting TestResultComposeData -MonitorName $newResult.Monitor.Name
            if(-not [string]::IsNullOrEmpty($ComposeDataSb)){
                if($ComposeDataSb.GetType().name -eq 'string'){
                    $ComposeDataSb = [ScriptBlock]::Create($ComposeDataSb)
                }
                $composeDataResult = Invoke-Command -ScriptBlock $ComposeDataSb -ArgumentList $result
                if($null -ne $composeDataResult -and $composeDataResult.GetType().Name -eq 'ScriptBlock'){
                    #Sometimes storing a scriptblock in a psd1 file makes the scriptblock get nested - so running it once will just return
                    #the block you actually want to run
                    $composeDataResult = Invoke-Command -ScriptBlock $composeDataResult -ArgumentList $result
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
            }
        }
        catch{
            throw
        }

        $newResult
    }
}