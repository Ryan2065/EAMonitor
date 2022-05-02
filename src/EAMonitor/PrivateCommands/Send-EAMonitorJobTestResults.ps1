Function Send-EAMonitorJobTestResults{
    Param(
        [EAMonitor.Classes.EAMonitorResult[]]$results
    )
    $FailedResults = @($results | where-object { $false -eq $_.TestResult.Passed })
    if($FailedResults.Count -eq 0) { return }
    $FailedResultHash = @{}
    #First - group by monitor
    foreach($FailedResult in $FailedResults){
        if(-not $FailedResultHash.ContainsKey($FailedResult.Monitor.Name)){
            $FailedResultHash[$FailedResult.Monitor.Name] = @()
        }
        $FailedResultHash[$FailedResult.Monitor.Name] += $FailedResult
    }
    $EnvironmentString = ''
    if(-not [string]::IsNullOrEmpty($Script:EAMonitorEnvironment)){
        $EnvironmentString = " for environment $($Script:EAMonitorEnvironment)"
    }
    $ReportBody = "Please review the failed monitors$($EnvironmentString).`n"
    foreach($key in $FailedResultHash.Keys){
        $FailedResultArray = @($FailedResultHash[$key])
        $MonitorName = $FailedResultArray[0].Monitor.Name
        $ReportBody += $FailedResultArray.Data | ConvertTo-Html -Fragment -PreContent "<h2>$($MonitorName)</h2>"
    }
    $Header = "
    <style>
    h2 {

        font-family: Arial, Helvetica, sans-serif;
        color: #000099;
        font-size: 16px;

    }
    table {
		font-size: 12px;
		border: 0px; 
		font-family: Arial, Helvetica, sans-serif;
	} 
	
    td {
		padding: 4px;
		margin: 0px;
		border: 0;
	}
	
    th {
        background: #395870;
        background: linear-gradient(#49708f, #293f50);
        color: #fff;
        font-size: 11px;
        text-transform: uppercase;
        padding: 10px 15px;
        vertical-align: middle;
	}

    tbody tr:nth-child(even) {
        background: #f0f0f2;
    }
    </style>
    "

    $Report = ConvertTo-HTML -Body $ReportBody -Title "EAMonitor - Failed Monitors" -Head $header

    $emailMessage = New-Object System.Net.Mail.MailMessage( 'Monitor@EphingAdmin.com' , 'Ryan@eph-it.com' )
    $emailMessage.Subject = "EAMonitor - Failed Monitors" 
    $emailMessage.IsBodyHtml = $true
    $emailMessage.Body = "$Report"
    $SMTPClient = New-Object System.Net.Mail.SmtpClient( 'smtp-relay.sendinblue.com' , 587 )
    $SMTPClient.EnableSsl = $true
    $SMTPClient.Credentials = New-Object System.Net.NetworkCredential( 'ryan2065@gmail.com' , $env:smtpkey );
    $SMTPClient.Send( $emailMessage )
}

