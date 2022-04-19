Function Register-EAMonitorEnvironment{
    [CmdletBinding()]
    Param(
        [string]$Name
    )
    $EnvironmentObject = Search-EFPosh -Entity $Script:EAMonitorDbContext.EAMonitorEnvironment -Expression { $_.Name -eq $0 } -Arguments @(,$Name) -FirstOrDefault
    if($null -ne $EnvironmentObject){
        return $EnvironmentObject
    }
    $tempenvObject = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorEnvironment
    $tempenvObject.Name = $Name
    Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $tempenvObject
    Save-EAMonitorContext
    return ( Search-EFPosh -Entity $Script:EAMonitorDbContext.EAMonitorEnvironment -Expression { $_.Name -eq $0 } -Arguments @(,$Name) -FirstOrDefault )
}