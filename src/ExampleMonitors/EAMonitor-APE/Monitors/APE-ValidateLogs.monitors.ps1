Param($MonitorSettings)

BeforeDiscovery{
    $PreviousJobs = Get-EAMonitorJob -MonitorName "APE-ValidateLogs" -MaxResults 2 -NewestFirst

    $LastRunTime = [DateTime]::MinValue
    if($PreviousJobs.Count -gt 1){
        $LastRunTime = $PreviousJobs[1].Created
    }
    write-host "Enabled: $($MonitorSettings.Enabled)"
    #Get APE Log and parse it to get structured data
    $RawAPE = Get-Content "\\Lab-CM.Home.Lab\C$\APE\APELog.log" -Raw
    $split = $RawAPE -Split [regex]::Escape("<![LOG[")
    
    $APEParseResults = @()

    foreach($s in $split){
        if([string]::IsNullOrEmpty($s)){ continue }
        $NextSplit = $s -split [regex]::Escape(']LOG]!>')
        $Message = $NextSplit[0]
        $LastSplit = $NextSplit.Split('"')
        for($i = 0; $i -lt $LastSplit.Count; $i++){
            if($LastSplit[$i] -like '*time*'){
                $Time = $LastSplit[$i+1]
            }
            elseif($LastSplit[$i] -like '*date*'){
                $Date = $LastSplit[$i+1]
            }
            elseif($LastSplit[$i] -like '*type*'){
                $Type = $LastSplit[$i+1]
            }
        }
        
        $DateTimeOfLog = Get-Date "$($Date) $($Time)"
        if($DateTimeOfLog -gt $LastRunTime -and $Type -eq 3){
            $APEParseResults += @(
                New-Object PSCustomObject @{
                    'Message' = $Message
                    'DateTime' = $DateTimeOfLog
                    'Type' = $Type
                }
            )
        }
    }
}

Describe "APE logs should contain no errors <_.Type>" -ForEach $APEParseResults {
        It 'Should contain no error'{
            $_.Type | Should -not -be 3
        }
}
