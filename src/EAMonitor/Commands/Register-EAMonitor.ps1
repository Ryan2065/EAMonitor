Function Register-EAMonitor{
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$Path
    )
    begin{
        New-EAMonitorDbContext
        if($null -eq $Script:RegisteredMonitors){
            $Script:RegisteredMonitors = New-Object System.Collections.Generic.List[RegisteredEAMonitor]
        }
    }
    process{
        if(-not $path.ToLower().EndsWith('tests.ps1')){
            throw [System.Management.Automation.PSNotSupportedException] "The file $($path) is not a valid Pester file ending in .tests.ps1"
            return
        }
        elseif(-not ( Test-Path $path -ErrorAction SilentlyContinue)){
            throw [System.Management.Automation.PSNotSupportedException] "Could not find the file path $($path)"
            return
        }
        $MonitorName = (Get-Item $path).BaseName.TrimEnd('.tests')
        
        $monObj = Get-EAMonitor -Name $MonitorName
        if($null -eq $monObj){
            New-EAMonitor -Name $MonitorName -Path $Path
            $monObj = Get-EAMonitor -Name $MonitorName
        }
        $registeredMonitor = [RegisteredEAMonitor]::new()
        $registeredMonitor.Name = $MonitorName
        $registeredMonitor.FilePath = $Path
        $registeredMonitor.DbMonitorObject = $monObj
        $Script:RegisteredMonitors.Add($registeredMonitor)
    }
    end{

    }
}