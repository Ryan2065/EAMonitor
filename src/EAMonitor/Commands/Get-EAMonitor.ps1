Function Get-EAMonitor{
    Param(
        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$Name
    )
    $SearchEFPoshParams = @{
        'Entity' = $Script:EAMonitorDbContext.EAMonitor
        'Include' =  @('Settings','Settings')
        'ThenInclude' =  @('SettingKey', 'SettingEnvironment')
        'ToList' = $true
    }
    if(-not ( [string]::IsNullOrEmpty($Name ))){
        return Search-EFPosh -Expression { $_.Name -eq $0 } -Arguments @(,$Name) @SearchEFPoshParams
    }
    else{
        return Search-EFPosh @SearchEFPoshParams
    }
}