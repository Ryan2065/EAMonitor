@{
    'TestResultComposeBlock' = {
        Param([Pester.Test]$Result)
        $ResultObject = @{}
        if($Result.Path[-1] -eq 'Is not running but is startup type automatic'){
            $ResultObject['Computer'] = $result.Block.Parent.Data.Computer
            $ResultObject['Service'] = $result.Block.Data.Name
            $ResultObject['ServiceStatus'] = $result.Block.Data.Status
        }
        elseif($Result.Path[-1] -eq 'Services should be found for the computer'){
            $ResultObject['Computer'] = $result.Block.Data.Computer
        }
        return $ResultObject
    }
}