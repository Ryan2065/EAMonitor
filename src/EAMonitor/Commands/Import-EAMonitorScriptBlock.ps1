Function Import-EAMonitorScriptBlock{
    [CmdletBinding()]
    Param(
        [ValidateSet('ProcessTestData','SendNotification')]
        [string]$Type,
        [string]$Name,
        [ScriptBlock]$ScriptBlock
    )
    if($null -eq $Script:EAMonitorActions){
        $Script:EAMonitorActions = New-Object EAMonitor.Classes.EAMonitorAction
    }
    $predicate = { param($astObject)  $astObject -is [System.Management.Automation.Language.ParamBlockAst] }
    $paramSb = $ScriptBlock.Ast.Find($predicate, $true)
    if($paramSb.Parameters.Count -ne 1){
        Write-Warning "Actions will be sent a single parameter. Number of parameters found for the script block: $($paramSb.Parameters.Count). Will not import"
        return
    }
    foreach($action in [EAMonitor.Classes.EAMonitorModuleCache]::Actions){
        if($action.Name -eq $Name -and $action.Type -eq $Type){
            throw "Action with name $Name already exists"
            return
        }
    }
    $newAction = New-Object EAMonitor.Classes.EAMonitorAction
    $newAction.Name = $Name
    $newAction.Type = $Type
    $newAction.Script = $ScriptBlock
    [EAMonitor.Classes.EAMonitorModuleCache]::Actions.Add($newAction)
}