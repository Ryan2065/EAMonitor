Function Import-EAMonitorSetting {
    Param(
        [string]$Key,
        [string]$Description
    )
    $SettingKeyObject = Search-EFPosh -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSettingKey -Expression { $_.Name -eq $Key } -FirstOrDefault
    if ($null -eq $SettingKeyObject) {
        $tempSettingKeyObject = New-EFPosh -DbContext $Script:EAMonitorDbContext -Entity EAMonitorSettingKey
        $tempSettingKeyObject.Name = $SettingKey
        $tempSettingKeyObject.Description = $Description
        Add-EFPoshEntity -DbContext $Script:EAMonitorDbContext -Entity $tempSettingKeyObject
    }
    else{
        $SettingKeyObject.Description = $Description
    }
    Save-EAMonitorContext
}