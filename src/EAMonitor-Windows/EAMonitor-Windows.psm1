Foreach($MonitorFile in (Get-ChildItem -Path "$PSScriptRoot\Monitors" -Filter '*.monitors.ps1' -ErrorAction SilentlyContinue)){
    Import-EAMonitor -Path $MonitorFile.FullName
}

$Commands = Get-ChildItem -Path "$PSScriptRoot\Commands" -Filter '*.ps1' -ErrorAction SilentlyContinue
Foreach($c in $Commands){
    . $c.FullName
}

Import-EAMonitorScriptBlock -Type ProcessTestData -Name 'WindowsServiceHealthProcessData' -ScriptBlock {
    Param([Pester.Test]$Result)
    if($Result.Path[-1] -eq 'Automatic service should be running'){
        return [PSCustomObject]@{
            Computer     = $result.Block.Parent.Data.Computer
            Service = $result.Block.Data.Name
            ServiceStatus    = $result.Block.Data.Status
        }
    }
}

Export-ModuleMember -Function $Commands.BaseName