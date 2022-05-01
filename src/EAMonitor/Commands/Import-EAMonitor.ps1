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
            $Script:ImportedMonitors = New-Object System.Collections.Generic.List[RegisteredEAMonitor]
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
        $Script:ImportedMonitors.Add([RegisteredEAMonitor]::new($Path))
    }
    end{

    }
}