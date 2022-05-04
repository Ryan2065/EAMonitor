Function Get-EAMonitorJob{
    Param(
        [string]$MonitorName,
        [int]$MaxResults,
        [switch]$NewestFirst
    )
    $SearchEFPoshParams = @{
        'DbContext' = $Script:EAMonitorDbContext
        'Entity' = 'EAMonitorJob'
        'Include' = 'Monitor','Tests'
        'Expression' = { $_.Monitor.Name -eq $MonitorName -and $null -ne $_.Completed }
    }
    if($MaxResults){
        $SearchEFPoshParams['Take'] = $MaxResults
    }
    if($NewestFirst){
        $SearchEFPoshParams['OrderByDescending'] = 'Created'
    }
    Search-EFPosh @SearchEFPoshParams
}