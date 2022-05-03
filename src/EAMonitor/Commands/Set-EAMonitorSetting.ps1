Function Set-EAMonitorSetting{
    Param(
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$MonitorName,
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Key,
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Value
    )
    
    $SettingObject = $null
    $SearchEFPoshParams = @{
        'DbContext' = $Script:EAMonitorDbContext
        'Entity' = 'EAMonitorSetting'
        'FirstOrDefault' = $true
    }
    if(-not ([string]::IsNullOrEmpty($MonitorName))) {
        Write-Debug "Searching for setting $($Key) tied to monitor $($MonitorName)"
        $SettingObject = Search-EFPosh -Expression { $_.SettingKey.Name -eq $Key -and $_.Monitor.Name -eq $MonitorName } @SearchEFPoshParams
    }
    else{
        Write-Debug "Searching for default setting $($Key)"
        $SettingObject = Search-EFPosh -Expression { $_.SettingKey.Name -eq $Key } @SearchEFPoshParams | Where-Object { $null -eq $_.MonitorId }
    }
    if($null -ne $SettingObject -and $SettingObject.SettingValue -ne $Value){
        Write-Debug "Found existing setting object with Id $($SettingObject.Id) and the value needs updating."
        $SettingObject.SettingValue = $Value
        $SettingObject.LastModified = [DateTime]::UtcNow
        Save-EAMonitorContext
        return
    }
    elseif($null -ne $SettingObject){
        Write-Debug "Found existing setting object with Id $($SettingObject.Id) and the value is already equal to $($Value) - Exiting"
        return
    }

    #If monitor name provided - get the object or create it in the db
    $MonitorObject = $null
    if(-not [string]::IsNullOrEmpty($MonitorName)){
        $MonitorObject = Register-EAMonitor -MonitorName $MonitorName
    }
    
    #get the setting key or create it in the db
    $KeyObject = Search-EFPosh -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSettingKey -Expression { $_.Name -eq $Key } -FirstOrDefault
    if($null -eq $KeyObject){
        $tempSettingKeyObject = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSettingKey
        $tempSettingKeyObject.Name = $Key
        Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $tempSettingKeyObject
        Save-EAMonitorContext
        $KeyObject = Search-EFPosh -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSettingKey -Expression { $_.Name -eq $Key } -FirstOrDefault
    }
    if($null -eq $KeyObject){
        throw "Could not get or create the setting key object"
    }

    $NewSetting = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSetting
    $NewSetting.SettingValue = $Value
    $NewSetting.LastModified = [DateTime]::UtcNow
    $NewSetting.SettingKeyId = $KeyObject.Id
    if($null -ne $MonitorObject){
        $NewSetting.MonitorId = $MonitorObject.Id
    }
    Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $NewSetting
    Save-EAMonitorContext
}