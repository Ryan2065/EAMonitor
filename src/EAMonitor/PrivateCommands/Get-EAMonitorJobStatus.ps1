Function Get-EAMonitorJobStatus{
    Param(
        [string]$Name
    )
    $jobStatuses = Get-EAMonitorCachedData -Name 'JobStatuses' -ActiveFor ( New-TimeSpan -Minutes 20 ) -ScriptBlock {
        Write-Debug "Retrieving all statuses from DB"
        $SearchArguments = @{
            'ToList' = $true
            'Entity' = $Script:EAMonitorDbContext.EAMonitorJobStatus
        }
        Search-EFPosh @SearchArguments
    }
    foreach($s in $jobStatuses){
        if($s.Name -eq $Name){
            return $s
        }
    }
}