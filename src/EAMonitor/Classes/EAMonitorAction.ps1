Class EAMonitorAction{
    [string]$ActionType
    [string]$Name
    [scriptblock]$Action
    EAMonitorAction($ActionType, $Name, $Action){
        $this.Action = $Action
        $this.Name = $Name
        $this.ActionType = $ActionType
    }
}