Param($buildEFPosh = $true)

$ErrorActionPreference = 'Stop'

if($buildEFPosh){
    #. C:\Users\Ryan2\OneDrive\Code\EFPosh\build.ps1
}

$RebuildBinaries = $true
$rerunMigrations = $true

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

}

$PublicCommands = (Get-ChildItem "$PSScriptRoot\EAMonitor\Commands" -Filter '*.ps1').BaseName
Update-ModuleManifest -Path "$PSScriptRoot\EAMonitor\EAMonitor.psd1" -FunctionsToExport $PublicCommands

#region test it out
