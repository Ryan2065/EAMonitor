Function Send-EAMonitorJobTestResults{
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

        $TestResultComposeBlock = Get-EAMonitorSetting -MonitorName $MonitorObject.Name -Setting 'TestResultComposeBlock'
        if($null -eq $TestResultComposeBlock){
            Write-Warning "Could not parse results for test file $($MonitorFile)"
            continue
        }
        $ResultHash = Invoke-Command -ScriptBlock $TestRestultComposeBlock -ArgumentList $result
        $ResultHash['Passed'] = $result.Passed
        
    }
}

