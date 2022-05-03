Function Initialize-EAMonitor{
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory = $true, ParameterSetName = "Sqlite")]
        [string]$SqliteFilePath,
        [Parameter(Mandatory = $true, ParameterSetName = "Sql")]
        [string]$SqlServer,
        [Parameter(Mandatory = $true, ParameterSetName = "Sql")]
        [string]$Database,
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [bool]$IntegratedSecurity = $false,
        [Parameter(Mandatory = $true, ParameterSetName = "ConnectionString")]
        [string]$ConnectionString,
        [Parameter(Mandatory = $true, ParameterSetName = "ConnectionString")]
        [ValidateSet('MSSQL', 'SQLite')]
        [string]$DatabaseType,
        [Parameter(Mandatory = $false, ParameterSetName = "ConnectionString")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sqlite")]
        [switch]$DetectMonitorModules,
        [Parameter(Mandatory = $false, ParameterSetName = "ConnectionString")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sqlite")]
        [switch]$CreateDb,
        [Parameter(Mandatory = $false, ParameterSetName = "ConnectionString")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sqlite")]
        [string]$Environment
    )
    begin{
        $Script:efPoshDbContextParams = @{
            'EnsureCreated' = $true -eq $CreateDb
            'RunMigrations' = $true -eq $CreateDb
        }
        $ParentDirectory = (Get-Item $PSScriptRoot).Parent
        [EAMonitor.Classes.EAMonitorModuleCache]::Clear()
    }
    Process{
        if($PSVersionTable.PSVersion.Major -gt 5){
            $Script:efPoshDbContextParams['AssemblyFile'] = [System.IO.Path]::Combine($ParentDirectory.FullName, "Dependencies", "net6.0", "EAMonitor.dll")
        }
        else{
            $Script:efPoshDbContextParams['AssemblyFile'] = [System.IO.Path]::Combine($ParentDirectory.FullName, "Dependencies", "net472", "EAMonitor.dll")
        }

        if($Environment){
            $Script:EAMonitorEnvironment = $Environment
        }
        
        if($PSCmdlet.ParameterSetName -eq 'Sqlite'){
            $Script:efPoshDbContextParams['SQLiteFile'] = $SqliteFilePath
            $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqlite'
            if($PSVersionTable.PSVersion.Major -le 5){
                $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqliteNet47'
            }
        }
        elseif($PSCmdlet.ParameterSetName -eq 'ConnectionString'){
            $Script:efPoshDbContextParams['DBType'] = $DatabaseType
            $Script:efPoshDbContextParams['ConnectionString'] = $ConnectionString
            if($DatabaseType -eq 'SQLite'){
                $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqlite'
                if($PSVersionTable.PSVersion.Major -le 5){
                    $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqliteNet47'
                }
            }
            elseif($DatabaseType -eq 'MSSQL'){
                $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSQL'
                if($PSVersionTable.PSVersion.Major -le 5){
                    $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSQLNet47'
                }
            }
        }
        elseif($PSCmdlet.ParameterSetName -eq 'Sql'){
            $Script:efPoshDbContextParams['MSSQLServer'] = $SqlServer
            $Script:efPoshDbContextParams['MSSQLDatabase'] = $Database
            $Script:efPoshDbContextParams['MSSQLIntegratedSecurity'] = $IntegratedSecurity
            $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSQL'
            if($PSVersionTable.PSVersion.Major -le 5){
                $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSQLNet47'
            }
        }
        else{
            throw "Congrats - this should be impossible! Open an issue and provide the parameters used to call this function"
            return
        }
        New-EAMonitorDbContext -Force
        
    }
    end{
        if($DetectMonitorModules){
            $ModulesToImport = Get-Module -Name 'EAMonitor-*' -ListAvailable
            foreach($module in $ModulesToImport){
                Import-Module $Module.Name -Force
            }
        }
        Import-EAMonitorScriptBlock -Type ProcessTestData -Name 'Default' -ScriptBlock {
            Param([Pester.Test]$Result)
            return [PSCustomObject]@{
                Test = $result.ExpandedPath
                Passed = $result.Passed
            }
        }
        Import-EAMonitorScriptBlock -Type 'SendNotification' -Name 'Default' -ScriptBlock {
            Param([EAMonitor.Classes.EAMonitorResult[]]$FailedResultArray)
            $MonitorName = $FailedResultArray[0].Monitor.Name
            $monSettings = Get-EAMonitorSetting -MonitorName $MonitorName -AsHashtable

            if([string]::IsNullOrEmpty($monSettings.SendMailTo)){
                return;
            }
            
            $header = '<style>h2{font-family:Arial,Helvetica,sans-serif;color:#009;font-size:16px}table{font-size:12px;border:0;font-family:Arial,Helvetica,sans-serif}td{padding:4px;margin:0;border:0}th{background:#395870;background:linear-gradient(#49708f,#293f50);color:#fff;font-size:11px;text-transform:uppercase;padding:10px 15px;vertical-align:middle}tbody tr:nth-child(even){background:#f0f0f2}</style>'
            $env = Get-EAMonitorEnvironment
            $EnvironmentString = ''
            if(-not [string]::IsNullOrEmpty($env)){
                $EnvironmentString = " for environment $($env)"
            }
            $ReportBody = "Please review the failed monitor $($MonitorName) $($EnvironmentString). The test failures are:`n"
            $CompiledResults = @{}
            foreach($result in $FailedResultArray){
                if($CompiledResults[$result.TestResult.Name]){
                    $CompiledResults[$result.TestResult.Name] += $result
                }
                else{
                    $CompiledResults[$result.TestResult.Name] = @($result)
                }
            }
            foreach($key in $CompiledResults.Keys){
                $compiledResultsArray = $CompiledResults[$key]
                $ReportBody += $compiledResultsArray.Data | ConvertTo-Html -Fragment -PreContent "<h2>Test: $($key)</h2>"
            }

            $Report = ConvertTo-HTML -Body $ReportBody -Title "EAMonitor - Failed Monitors" -Head $header
            
            $emailMessage = New-Object System.Net.Mail.MailMessage
            $emailMessage.From = $monSettings.SendMailFrom
            foreach($email in $monSettings.SendMailTo.Split(';').Split(',')){
                $emailMessage.To.Add($email)
            }
            $emailMessage.Subject = "EAMonitor - Failed Monitors" 
            $emailMessage.IsBodyHtml = $true
            $emailMessage.Body = "$Report"
            $SMTPClient = New-Object System.Net.Mail.SmtpClient( $monSettings.SendMailSmtp , $monSettings.SendMailSmtpPort)
            if($monSettings.SendMailEnableSSl){
                $SMTPClient.EnableSsl = $monSettings.SendMailEnableSSl
            }
            if($MonSettings.SendMailCredentials){
                $Secret = Get-Secret -Name $MonSettings.SendMailCredentials
                $SMTPClient.Credentials = New-Object System.Net.NetworkCredential( $Secret.GetNetworkCredential().UserName, $Secret.GetNetworkCredential().Password );
            }
            
            $SMTPClient.Send( $emailMessage )
        }
    }
}