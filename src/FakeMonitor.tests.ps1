

Describe "This is a fake monitor to test EAMonitor"{
    Context "This is the context string"{
        $ParamArray = @()
        $ParamArray += @{
            'VarOne' = "1"
        }
        foreach($paramSet in $ParamArray){
            It "<VarOne> this is the message" -TestCases $paramSet{
                Get-EAMonitor -Name 'FakeMonitor'
                $VarOne | Should -be 1
            }
            It "<VarOne> this is the second message" -TestCases $paramSet{
                $VarOne | Should -be 1
            }
        }
    }
}