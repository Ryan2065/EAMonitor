Function Import-EAMonitor{
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory=$true)]
        [ValidateNotNullOrEmpty()]
        [string]$Path
    )
    begin{
        New-EAMonitorDbContext
        if($null -eq $Script:ImportedMonitors){
            $Script:ImportedMonitors = New-Object System.Collections.Generic.List[EAMonitor.Classes.EAMonitorRegistered]
        }
    }
    process{
        if(-not $path.ToLower().EndsWith('monitors.ps1')){
            throw [System.Management.Automation.PSNotSupportedException] "The file $($path) is not a valid EAMonitor file ending in .monitors.ps1"
            return
        }
        elseif(-not ( Test-Path $path -ErrorAction SilentlyContinue)){
            throw [System.Management.Automation.PSNotSupportedException] "Could not find the file path $($path)"
            return
        }
        $RegisteredMonitor = [EAMonitor.Classes.EAMonitorRegistered]::new($Path)
        $LocalSettings = Get-EAMonitorLocalSettings -MonitorName $RegisteredMonitor.Name -Directory $RegisteredMonitor.Directory -Environment $Script:EAMonitorEnvironment
        $RegisteredMonitor.DbMonitorObject = Register-EAMonitor -MonitorName $RegisteredMonitor.Name -Description $localSettings['Description']
        $Script:ImportedMonitors.Add($RegisteredMonitor)
    }
    end{

    }
}