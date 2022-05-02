
BeforeDiscovery{
    # Monitors windows service health
    $MonitorSettings = Get-EAMonitorSetting -MonitorName 'Windows-ServiceHealth'
    
    $ComputersToMonitor = $MonitorSettings."WindowsServiceHealth-ComputerList"
    $SecretToUse = $MonitorSettings."WindowsServiceHealth-SecretToUse"

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
                Get-Service | Where-Object { $_.StartType -eq 'Automatic' }
            }
        }
        
        $Tests += @{
            'Computer' = $computer
            'Services' = $result
        }
    }
}


Describe "Computer <_.Computer>" -ForEach $Tests {
    It "Computer should be online and return a list of services"{
        $_.Services | Should -not -be $null
    }
    Context "Service <_.Name>" -Foreach $_.Services {
        It "Automatic service should be running" {
            $_.Status | Should -be 'Running'
        }
    }
}