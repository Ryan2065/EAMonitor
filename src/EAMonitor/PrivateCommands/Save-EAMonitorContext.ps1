Function Save-EAMonitorContext{
    Param()
    try{
        Save-EFPoshChanges -DbContext $Script:EAMonitorDbContext
    }
    catch{
        Get-EAMonitorError -ThrownException $_
    }
}