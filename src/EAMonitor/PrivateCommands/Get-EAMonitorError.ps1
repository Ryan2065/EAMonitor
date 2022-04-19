Function Get-EAMonitorError{
    Param(
        [Exception]$ThrownException
    )
    #When DbContexts error out, it could cache the change and then always re-try
    #If an error is encountered - refresh the db context so any new Db operations won't
    #have the bad cached change
    New-EAMonitorDbContext -Force

    $tempException = $ThrownException
    $count = 0
    while($null -ne $tempException.InnerException){
        $tempException = $tempException.InnerException
        $count++
        if($count -gt 20){
            throw $ThrownException
            return
        }
    }
    Write-Warning "Exception interacting with database. Base exception message $($tempException.Message)"
    throw $ThrownException
    return
}