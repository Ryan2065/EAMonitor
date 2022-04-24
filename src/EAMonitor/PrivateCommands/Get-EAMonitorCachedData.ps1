Function Get-EAMonitorCachedData{
    Param(
        [string]$Name,
        [ScriptBlock]$ScriptBlock,
        [TimeSpan]$ActiveFor
    )
    if($null -eq $Script:eaMonitorCachedData){
        $Script:eaMonitorCachedData = [System.Collections.Generic.Dictionary[string,EAMonitorCacheData]]::new()
    }
    $gottenValue = $null
    if($Script:eaMonitorCachedData.TryGetValue($Name, [ref]$gottenValue) ){
        if($gottenValue.ExpireAt -gt [DateTime]::UtcNow){
            return $gottenValue.CachedResult
        }
    }
    $NewCacheItem = [EAMonitorCacheData]::new()
    $NewCacheItem.ExpireAt = [DateTime]::UtcNow.AddSeconds($ActiveFor.TotalSeconds)
    $NewCacheItem.CachedResult = . $ScriptBlock
    $Script:eaMonitorCachedData[$Name] = $NewCacheItem
    return $NewCacheItem.CachedResult
}