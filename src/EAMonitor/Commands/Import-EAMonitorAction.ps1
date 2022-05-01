Function Import-EAMonitorAction {
    Param(
        [ValidateSet('TestResultCompose', 'JobCompose', 'SendNotification')]
        [string]$ActionType,
        [string]$Name,
        [ScriptBlock]$Action
    )
    if($null -eq $Script:EAMonitorActions){
        $Script:EAMonitorActions = New-Object System.Collections.Generic.List[EAMonitorAction]
    }
    $Script:EAMonitorActions.Add( [EAMonitorAction]::new($ActionType, $Name, $Action) )
}