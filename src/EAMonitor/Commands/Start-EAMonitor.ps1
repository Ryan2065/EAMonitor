Function Start-EAMonitor {
    Param(
        [Parameter(Mandatory = $false)]
        [string]$Name,
        [Parameter(Mandatory = $false)]
        [switch]$SkipSchedule
    )
    begin{
        $MonitorsToRun = $Script:ImportedMonitors

        $SearchEFPoshParams = @{
            'Entity' = $Script:EAMonitorDbContext.EAMonitor
            'Include' =  @('Settings','Settings')
            'ThenInclude' =  @('SettingKey', 'SettingEnvironment')
            'ToList' = $true
        }

        if(-not [string]::IsNullOrWhiteSpace($Name)){
            $SearchEFPoshParams['Expression'] = { $_.Name -eq $0 }
            $SearchEFPoshParams['Arguments'] = @(,$Name)
            foreach($mon in $Script:ImportedMonitors){
                if($mon.Name -eq $Name){
                    $MonitorsToRun = $mon
                }
            }
        }

        $RegisteredMonitorsToRun = New-Object System.Collections.Generic.List[RegisteredEAMonitor]
    }
    process{
        $MonitorDbRecords = Search-EFPosh @SearchEFPoshParams
    }
    end{}
}