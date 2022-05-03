Function Get-EAMonitorState{
    param(
        [Parameter(Mandatory=$true)]
        [ValidateSet('Unknown', 'Up', 'Down', 'Warning')]
        [string]$StateName
    )
    $monitorStates = Get-EAMemoryCacheValue -Key 'MonitorStates' -ActiveFor ( New-TimeSpan -Minutes 20 ) -Action {
        Search-EFPosh -DbContext $Script:EAMonitorDbContext -Entity 'EAMonitorState' -ToList
    }
    foreach($state in $monitorStates){
        if($state.Name -eq $StateName){ return $state }
    }
}