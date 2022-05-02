@{
    'TestResultComposeData' = {
        Param([Pester.Test]$Result)
        if($Result.Path[-1] -eq 'Automatic service should be running'){
            return [PSCustomObject]@{
                Computer     = $result.Block.Parent.Data.Computer
                Service = $result.Block.Data.Name
                ServiceStatus    = $result.Block.Data.Status
            }
        }
    }
}