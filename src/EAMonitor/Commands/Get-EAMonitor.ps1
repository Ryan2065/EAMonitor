Function Get-EAMonitor{
    Param(
        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$Name
    )
    $SearchEFPoshParams = @{
        'DbContext' = $Script:EAMonitorDbContext
        'Entity' = 'EAMonitor'
        'Include' =  @('Settings')
        'ThenInclude' =  @('SettingKey')
        'ToList' = $true
    }
    if(-not ( [string]::IsNullOrEmpty($Name ))){
        return Search-EFPosh -Expression { $_.Name -eq $Name } @SearchEFPoshParams
    }
    else{
        return Search-EFPosh @SearchEFPoshParams
    }
}