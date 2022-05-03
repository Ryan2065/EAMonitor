Param($buildEFPosh = $true)

$ErrorActionPreference = 'Stop'

if($buildEFPosh){
    #. C:\Users\Ryan2\OneDrive\Code\EFPosh\build.ps1
}


$RebuildBinaries = $true
$RerunMigrations = $false

$LastBuildTimeFile = [System.IO.Path]::Combine($($env:temp), "EAMonitorLastBuildTime.txt")
if(Test-Path $LastBuildTimeFile -ErrorAction SilentlyContinue){
  try{
    $LastBuildTime = Get-Content $LastBuildTimeFile | ConvertFrom-JSON
    if($LastBuildTime.DateTime){ $LastBuildTime = $LastBuildTime.DateTime }
    $MigrationFilesToCheck = Get-ChildItem "$PSScriptRoot\EAMonitorDb\EAMonitorDb" -Recurse -Depth 2 -Filter '*.cs' -File
    $MigrationFilesToCheck += Get-ChildItem "$PSScriptRoot\EAMonitorDb\EAMonitorDb\SqlFiles" -File
    $MigrationFilesToCheck += Get-ChildItem "$PSScriptRoot\EAMonitorDb\EAMonitorDb\SqliteFiles" -File
    $FilesToCheck = Get-ChildItem "$PSScriptRoot\EAMonitorDb\EAMonitorDb\Migrations" -File -Recurse
    $RebuildBinaries = $false
    foreach($file in $FilesToCheck){
      if($LastBuildTime -lt $file.LastWriteTime){
        $RebuildBinaries = $true
      }
    }
    foreach($file in $MigrationFilesToCheck){
        if($LastBuildTime -lt $file.LastWriteTime){
            $RerunMigrations = $true
            $RebuildBinaries = $true
        }
    }
  }
  catch{ throw }
  
}
$RebuildBinaries = $true
if($RebuildBinaries){
    if($RerunMigrations){
        . "$PSScriptRoot\EAMonitorDb\EAMonitorDb\AddMigration.ps1"
    }
    . "$PSScriptRoot\EAMonitorDb\EAMonitorDb\build.ps1"

    $DependencyFolder = "$PSScriptRoot\EAMonitor\Dependencies"

    try{
      $null = Remove-Item $DependencyFolder -Force -Recurse 
    }
    catch{
      cmd /c rd $DependencyFolder /S /Q
    }
    

    $null = New-Item $DependencyFolder -ItemType Directory -Force
    $null = New-Item "$DependencyFolder\net472" -ItemType Directory -Force
    $null = New-Item "$DependencyFolder\net6.0" -ItemType Directory -Force

    $null = Copy-Item -Path "$PSScriptRoot\EAMonitorDb\EAMonitorDb\bin\release\net472\publish\*" -Destination "$DependencyFolder\net472\"  -Force -Recurse
    $null = Copy-Item -Path "$PSScriptRoot\EAMonitorDb\EAMonitorDb\bin\release\net6.0\publish\*" -Destination "$DependencyFolder\net6.0"  -Force -Recurse
    [DateTime]::Now | ConvertTo-JSON | Out-File $LastBuildTimeFile -Force
}

$PublicCommands = (Get-ChildItem "$PSScriptRoot\EAMonitor\Commands" -Filter '*.ps1').BaseName
Update-ModuleManifest -Path "$PSScriptRoot\EAMonitor\EAMonitor.psd1" -FunctionsToExport $PublicCommands

#region test it out
