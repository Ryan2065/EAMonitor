Function New-EAMonitorDbContext{
    <#
    .SYNOPSIS
    Creates a new Db context based on parameters supplied to Initialize-EAMonitor
    
    .DESCRIPTION
    Creates a new Db context. Db contexts are meant to be short-lived, so it might need to be periodically refreshed
    
    .PARAMETER Force
    Switch to create a new one if the old one still exists
    
    .EXAMPLE
    New-EAMonitorDbContext
    
    .NOTES
    .Author: Ryan Ephgrave
    #>
    [CmdletBinding()]
    Param(
        [switch]$Force
    )
    $RunNew = $true -eq $Force
    if($null -eq $Script:EAMonitorDbContext -and $null -ne $Script:efPoshDbContextParams){
        $RunNew = $true
    }
    elseif($null -eq $Script:efPoshDbContextParams){
        throw "Initialize-EAMonitor must be run to get database connection information"
    }
    if($RunNew){
        $strDbSettings = @()
        foreach($key in $Script:efPoshDbContextParams.Keys){
            $value = $Script:efPoshDbContextParams[$key]
            if($key -eq 'ConnectionString'){
                $Value = '**************************'
            }
            $strDbSettings += "Parameter: $($Key)  Value: $($value)"
        }
        Write-Verbose "Creating DbContext based on Initialize settings:`n$($strDbSettings -join [System.Environment]::NewLine)"
        $Script:EAMonitorDbContext = New-EFPoshContext @Script:efPoshDbContextParams
    }
}