Function Get-EAMonitor{
    Param(
        [Parameter(Mandatory=$true, ParameterSetName="Name")]
        [Parameter(Mandatory=$false, ParameterSetName="Scheduled")]
        [ValidateNotNullOrEmpty()]
        [string]$Name,
        [Parameter(Mandatory=$true, ParameterSetName="Scheduled")]
        [Parameter(Mandatory=$false, ParameterSetName="Name")]
        [switch]$ScheduledToRunNow
    )
    $SearchEFPoshParams = @{
        'Entity' = $Script:EAMonitorDbContext.EAMonitor
        'Include' =  @('Settings','Settings')
        'ThenInclude' =  @('SettingKey', 'SettingEnvironment')
        'ToList' = $true
    }
    if($PSCmdlet.ParameterSetName -eq 'Name' -and (-not $ScheduledToRunNow.IsPresent)){
        return Search-EFPosh -Expression { $_.Name -eq $0 } -Arguments @(,$Name) @SearchEFPoshParams
    }

    if($PSCmdlet.ParameterSetName -eq 'Name' -and $ScheduledToRunNow.IsPresent){
        $SearchEFPoshParams['Expression'] = { $_.Name -eq $0 -and $_.NextRun -le $1}
        $SearchEFPoshParams['Arguments'] = @(,$Name, [DateTime]::UtcNow) 
    }
    else{
        $SearchEFPoshParams['Expression'] = { $_.NextRun -le $0 }
        $SearchEFPoshParams['Arguments'] = @(,[DateTime]::UtcNow) 
    }
    return Search-EFPosh @SearchEFPoshParams
}