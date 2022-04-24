
BeforeDiscovery{
    # Monitors windows service health
    $MonitorSettings = Get-EAMonitorSettings -MonitorName 'Windows-ServiceHealth'

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
        $result = Invoke-Command -ComputerName $computer @InvokeCommandSettings -ErrorAction SilentlyContinue -ScriptBlock {
            Get-Service | Where-Object { $_.StartType -eq 'Automatic' }
        }
        $Tests += @{
            'Computer' = $computer
            'Services' = $result
        }
    }
}


Describe "Computer <_.Computer>" -ForEach $Tests {
    It "Services should be found for the computer"{
        $_.Services | Should -not -be $null
    }
    Context " Service <_.Name>" -Foreach $_.Services {
        It " Is not running but is startup type automatic" {
            $_.Status | Should -be 'Running'
        }
    }
}