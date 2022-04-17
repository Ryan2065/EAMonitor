Function Get-EAMonitorState{
    param(
        [Parameter(Mandatory=$true)]
        [ValidateSet('Unknown', 'Up', 'Down', 'Warning')]
        [string]$StateName
    )
    if($null -eq $Script:EAMonitorStates){
        $Script:EAMonitorStates = Search-EFPosh -Entity $Script:EAMonitorDbContext.EAMonitorState
    }
    foreach($state in $Script:EAMonitorStates){
        if($state.Name -eq $StateName){ return $state }
    }
}