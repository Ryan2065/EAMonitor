BeforeDiscovery{
    $MonitorSettings = Get-EAMonitorSetting -MonitorName 'Windows-OSHealth' -AsHashtable
    
    $ComputersToMonitor = $MonitorSettings."WindowsOSHealth-ComputerList"
    $SecretToUse = $MonitorSettings."WindowsOSHealth-SecretToUse"

    if([string]::IsNullOrEmpty($ComputersToMonitor)){
        Write-Warning 'No computers to monitor - exiting'
        return
    }

    $InvokeCommandSettings = @{}
    if(-not [string]::IsNullOrEmpty($SecretToUse)){
        $InvokeCommandSettings = @{
            'Credential' = Get-Secret -Name $SecretToUse
        }
    }

    $Tests = @()
    Foreach($computer in $ComputersToMonitor.Split(",")){
        $computer = $computer.Trim()
        $result = $null
        if(Test-Connection -ComputerName $computer -Count 1 -ErrorAction 'SilentlyContinue'){
            $result = Invoke-Command -ComputerName $computer @InvokeCommandSettings -ErrorAction SilentlyContinue -ScriptBlock {
                Get-CimInstance -ClassName Win32_LogicalDisk -Filter "DriveType = 3" | Select-Object -Property 'DeviceID','FreeSpace','Size'
            }
        }
        $Tests += @{
            'Computer' = $computer
            'DiskInformation' = $result
        }
    }
}