Function Send-EAMonitorJobTestResults{
    Param(
        [EAMonitor.Classes.EAMonitorResult[]]$results
    )
    $FailedResults = @($results | where-object { $false -eq $_.TestResult.Passed })
    if($FailedResults.Count -eq 0) { return }
    $FailedResultHash = @{}
    #First - group by monitor
    foreach($FailedResult in $FailedResults){
        if(-not $FailedResultHash.ContainsKey($FailedResult.Monitor.Name)){
            $FailedResultHash[$FailedResult.Monitor.Name] = @()
        }
        $FailedResultHash[$FailedResult.Monitor.Name] += $FailedResult
    }

    
    foreach($key in $FailedResultHash.Keys){
        $FailedResultArray = @($FailedResultHash[$key])
        
        $MonitorName = $FailedResultArray[0].Monitor.Name
        $SendActions = @(Get-EAMonitorAction -MonitorName $MonitorName -Type 'SendNotification')
        foreach($SendAction in $SendActions){
            try{
                $null = Invoke-Command -ScriptBlock $SendAction.Script -ArgumentList @(,$FailedResultArray)
            }
            catch{
                Write-Error "Couldn't send notification $($SendAction.Name) for monitor $($MonitorName)" -Exception $_ -ErrorAction Continue
                continue
            }
        }
    }
}

