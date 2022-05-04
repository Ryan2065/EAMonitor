Foreach($MonitorFile in (Get-ChildItem -Path "$PSScriptRoot\Monitors" -Filter '*.monitors.ps1' -Recurse -ErrorAction SilentlyContinue)){
    Import-EAMonitor -Path $MonitorFile.FullName
}

$Commands = Get-ChildItem -Path "$PSScriptRoot\Commands" -Filter '*.ps1' -ErrorAction SilentlyContinue
Foreach($c in $Commands){
    . $c.FullName
}


Import-EAMonitorScriptBlock -Type ProcessTestData -Name 'APECheckLogProcessData' -ScriptBlock {
    Param([Pester.Test]$Result)
    if($Result.Path[-1] -eq 'Should contain no error'){
        return [PSCustomObject]@{
            DateTime = $result.Block.Parent.Data.DateTime
            Message     = $result.Block.Parent.Data.Message
        }
    }
}


Export-ModuleMember -Function $Commands.BaseName