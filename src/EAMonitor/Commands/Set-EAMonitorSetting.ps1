Function Set-EAMonitorSetting{
    Param(
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$MonitorName,
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$SettingKey,
        [Parameter(Mandatory = $true)]
        [ValidateNotNullOrEmpty()]
        [string]$Value,
        [Parameter(Mandatory = $false)]
        [ValidateNotNullOrEmpty()]
        [string]$Environment
    )
    
    $EnvironmentObject = $null
    if(-not [string]::IsNullOrEmpty($Environment)){
        $EnvironmentObject = Register-EAMonitorEnvironment -Name $Environment
    }
    
    $SettingObject = $null
    $SearchEFPoshParams = @{
        'Entity' = $Script:EAMonitorDbContext.EAMonitorSetting
        'FirstOrDefault' = $true
    }
    if(-not ([string]::IsNullOrEmpty($MonitorName)) -and $null -ne $EnvironmentObject) {
        Write-Debug "Searching for setting $($SettingKey) tied to monitor $($MonitorName) for environment $($EnvironmentObject.Name)"
        $SettingObject = Search-EFPosh -Expression { $_.SettingKey.Name -eq $0 -and $_.Monitor.Name -eq $1 -and $_.SettingEnvironmentId -eq $2 } -Arguments @(,$SettingKey,$MonitorName,$EnvironmentObject.Id) @SearchEFPoshParams
    }
    elseif(-not ([string]::IsNullOrEmpty($MonitorName))){
        Write-Debug "Searching for setting $($SettingKey) tied to monitor $($MonitorName) with no Environment information"
        $SettingObject = Search-EFPosh -Expression { $_.SettingKey.Name -eq $0 -and $_.Monitor.Name -eq $1 -and $_.SettingEnvironmentId -eq $2 } -Arguments @(,$SettingKey,$MonitorName,$null) @SearchEFPoshParams
    }
    elseif($null -ne $EnvironmentObject){
        Write-Debug "Searching for default setting $($SettingKey) for environment $($EnvironmentObject.Name)"
        $SettingObject = Search-EFPosh -Expression { $_.SettingKey.Name -eq $0 -and $_.MonitorId -eq $1 -and $_.SettingEnvironmentId -eq $2 } -Arguments @(,$SettingKey,$null,$EnvironmentObject.Id) @SearchEFPoshParams
    }
    else{
        Write-Debug "Searching for default setting $($SettingKey) with no Environment information"
        $SettingObject = Search-EFPosh -Expression { $_.SettingKey.Name -eq $0 -and $_.MonitorId -eq $1 -and $_.SettingEnvironmentId -eq $2 } -Arguments @(,$SettingKey,$null,$null) @SearchEFPoshParams
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
    $MonitorObject = Register-EAMonitor -Name $MonitorName

    #get the setting key or create it in the db
    $SettingKeyObject = Search-EFPosh -Entity $Script:EAMonitorDbContext.EAMonitorSettingKey -Expression { $_.Name -eq $0 } -Arguments @(,$SettingKey) -FirstOrDefault
    if($null -eq $SettingKeyObject){
        $tempSettingKeyObject = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSettingKey
        $tempSettingKeyObject.Name = $SettingKey

        Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $tempSettingKeyObject
        Save-EAMonitorContext
        $SettingKeyObject = Search-EFPosh -Entity $Script:EAMonitorDbContext.EAMonitorSettingKey -Expression { $_.Name -eq $0 } -Arguments @(,$SettingKey) -FirstOrDefault
        
    }
    if($null -eq $SettingKeyObject){
        throw "Could not get or create the setting key object"
    }

    $NewSetting = New-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSetting
    $NewSetting.SettingValue = $Value
    $NewSetting.LastModified = [DateTime]::UtcNow
    $NewSetting.SettingKeyId = $SettingKeyObject.Id
    if($null -ne $MonitorObject){
        $NewSetting.MonitorId = $MonitorObject.Id
    }
    if($null -ne $EnvironmentObject){
        $NewSetting.SettingEnvironmentId = $EnvironmentObject.Id
    }
    Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $NewSetting
    Save-EAMonitorContext
}