

Describe "This is another fake monitor to test EAMonitor"{
    BeforeAll{
        $VarOneValue = Get-EAMonitorSettings -MonitorName 'FakeMonitor2' -Setting 'VarOne'
        $ParamArray = @()
        $ParamArray += @{
            'VarOne' = $VarOneValue
        }
    }
    Context "This is the context string"{
        It "<VarOne> this is the message" -TestCases $ParamArray{
            Param([string]$VarOne)
            $VarOne | Should -be 1
        }
    }
}