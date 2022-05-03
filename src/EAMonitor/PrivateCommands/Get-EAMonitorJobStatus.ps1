Function Get-EAMonitorJobStatus{
    Param(
        [string]$Name
    )
    $jobStatuses = Get-EAMemoryCacheValue -Key 'JobStatuses' -ActiveFor ( New-TimeSpan -Minutes 20 ) -Action {
        Write-Debug "Retrieving all statuses from DB"
        $SearchArguments = @{
            'ToList' = $true
            'Entity' = 'EAMonitorJobStatus'
            'DbContext' = $Script:EAMonitorDbContext
        }
        Search-EFPosh @SearchArguments
    }
    foreach($s in $jobStatuses){
        if($s.Name -eq $Name){
            return $s
        }
    }
}