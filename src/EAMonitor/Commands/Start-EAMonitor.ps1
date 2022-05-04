Function Start-EAMonitor {
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory = $false)]
        [string]$Name,
        [Parameter(Mandatory = $false)]
        [switch]$SkipSchedule,
        [switch]$PassThru
    )
    begin{
        $Module = Import-Module -Name Pester -MinimumVersion 5.3.0 -PassThru -ErrorAction SilentlyContinue -Verbose:$false
        if($null -eq $Module){
            Write-Warning "wfMonitor was designed for Pester 5.3 and above. It was not found. Please install Pester 5.3.0 or higher.`nWill continue, but results may be incomplete."
        }
        $SearchEFPoshParams = @{
            'DbContext' = $Script:EAMonitorDbContext
            'Entity' = 'EAMonitorJob'
            'FirstOrDefault' = $true
            'OrderByDescending' = 'Created'
            'Include' = 'Monitor', 'Tests'
            'FromSql' = "
            SELECT eaj.*
            FROM EAMonitorJob eaj
            JOIN (
                SELECT eajJoin.MonitorId, MAX(eajJoin.Created) Created
                FROM EAMonitorJob eajJoin
                GROUP BY MonitorId) eajMax
            ON eaj.MonitorId = eajMax.MonitorId AND eaj.Created = eajMax.Created
            WHERE eajMax.MonitorId IS NOT NULL
            "
        }

        $tempImportedMonitors = $Script:ImportedMonitors

        if($null -eq $tempImportedMonitors -or $tempImportedMonitors.Count -eq 0){
            Write-Warning "No imported monitors found - stopping"
            return
        }

        if(-not [string]::IsNullOrWhiteSpace($Name)){
            foreach($mon in $Script:ImportedMonitors){
                if($mon.Name -eq $Name){
                    $tempImportedMonitors = @($mon)
                }
            }
        }
        $SearchEFPoshParams['Expression'] = { $tempImportedMonitors.Name -contains $_.Monitor.Name }
        
        $JobDbRecords = Search-EFPosh @SearchEFPoshParams

        $RegisteredMonitorsToRun = New-Object System.Collections.Generic.List[EAMonitor.Classes.EAMonitorRegistered]
    }
    process{
        $MonitorSettingHash = @{}
        $MonitorJobHash = @{}
        foreach($ImportedMonitor in $tempImportedMonitors){
            $Settings = Get-EAMonitorSetting -MonitorName $ImportedMonitor.Name -AsHashtable
            if('true' -ne $Settings.Enabled){
                Write-Debug "Monitor $($ImportedMonitor.Name) is not enabled - skipping"
                continue
            }
            if($SkipSchedule ){
                Write-Debug "Will run monitor $($ImportedMonitor.Name) - Schedule not evaluated since SkipSchedule was true"
                $RegisteredMonitorsToRun.Add($ImportedMonitor)
                continue
            }
            elseif(-not [string]::IsNullOrEmpty($Settings.RepeatMinuteInterval)){
                $LastRunTime = [DateTIme]::MinValue
                $LastJob = $null
                foreach($JobDbRecord in $JobDbRecords){
                    if($JobDbRecord.Monitor.Name -eq $ImportedMonitor.Name){
                        $LastRunTime = $JobDbRecord.Created
                        $LastJob = $JobRecord
                    }
                }
                $RepeatMinuteInterval = $null
                if([int]::TryParse($Settings.RepeatMinuteInterval, [ref]$RepeatMinuteInterval)){
                    $NextStartTime = $LastRunTime.AddMinutes($RepeatMinuteInterval)
                    if($NextStartTime -le [DateTime]::Now){
                        Write-Debug "Will run monitor $($ImportedMonitor.Name)"
                        $RegisteredMonitorsToRun.Add($ImportedMonitor)
                        continue
                    }
                }
                Write-Debug "Monitor $($ImportedMonitor.Name) is not due to run yet - skipping"
            }
            else{
                Write-Debug "$($ImportedMonitor.Name) has no schedule - so we'll skip it"
                continue
            }
        }
        if($RegisteredMonitorsToRun.Count -eq 0){
            Write-Debug "No monitors found needing to run - exiting"
            return
        }
        
        $jobs = New-EAMonitorJob -Monitors $RegisteredMonitorsToRun

        $ScriptFiles = $RegisteredMonitorsToRun.FilePath
        $PesterConfig = New-PesterConfiguration
        $PesterConfig.CodeCoverage.Enabled = $false
        $PesterConfig.Run.Path = $ScriptFiles
        $PesterConfig.Run.PassThru = $true
        $PesterConfig.Run.TestExtension = 'monitors'
        $results = Invoke-Pester -Path $ScriptFiles -PassThru @PesterParams
    }
    end{
        $EAMonitorResultObjects = ConvertTo-EAMonitorResult -Results $results -Monitors $RegisteredMonitorsToRun -Jobs $Jobs
        Save-EAMonitorJobTestResults -Results $EAMonitorResultObjects
        Send-EAMonitorJobTestResults -Results $EAMonitorResultObjects
        if($PassThru){
            return $results
        }
    }
}