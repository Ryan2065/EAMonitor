Param($buildEFPosh = $true)

$ErrorActionPreference = 'Stop'

if($buildEFPosh){
    . C:\Users\Ryan2\OneDrive\Code\EFPosh\build.ps1
}

$RebuildBinaries = $true
$RerunMigrations = $false

$LastBuildTimeFile = [System.IO.Path]::Combine($($env:temp), "EAMonitorLastBuildTime.txt")
if(Test-Path $LastBuildTimeFile -ErrorAction SilentlyContinue){
  try{
    $LastBuildTime = Get-Content $LastBuildTimeFile | ConvertFrom-JSON
    $MigrationFilesToCheck = Get-ChildItem "$PSScriptRoot\EAMonitorDb\EAMonitorDb" -File
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
if($RebuildBinaries){
    if($RerunMigrations){
        . "$PSScriptRoot\EAMonitorDb\EAMonitorDb\AddMigration.ps1"
    }
    . "$PSScriptRoot\EAMonitorDb\EAMonitorDb\build.ps1"

    $DependencyFolder = "$PSScriptRoot\EAMonitor\Dependencies"

    $null = Remove-Item $DependencyFolder -Force -Recurse -ErrorAction SilentlyContinue

    $null = New-Item $DependencyFolder -ItemType Directory -Force
    $null = New-Item "$DependencyFolder\net472" -ItemType Directory -Force
    $null = New-Item "$DependencyFolder\net6.0" -ItemType Directory -Force

    $null = Copy-Item -Path "$PSScriptRoot\EAMonitorDb\EAMonitorDb\bin\release\net472\publish\*" -Destination "$DependencyFolder\net472\"  -Force -Recurse
    $null = Copy-Item -Path "$PSScriptRoot\EAMonitorDb\EAMonitorDb\bin\release\net6.0\publish\*" -Destination "$DependencyFolder\net6.0"  -Force -Recurse
    [DateTime]::Now | ConvertTo-JSON | Out-File $LastBuildTimeFile -Force
}



$VerbosePreference = 'Continue'
$Global:DebugPreference = 'Continue'
Import-Module "C:\Users\Ryan2\OneDrive\Code\EFPosh\src\Module\EFPosh" -Force 
Import-Module "$PSScriptRoot\EAMonitor" -Force -Verbose:$false

$binPath = [System.IO.Path]::Combine($PSScriptRoot, "bin")
if(-not (Test-Path $binPath -ErrorAction SilentlyContinue)){
    $null = New-Item -ItemType Directory -Path $binPath -Force
}
foreach($file in Get-ChildItem $binPath -File){
    $null = Remove-Item $file.FullName -Force
}

$SqliteFile = [System.IO.Path]::Combine($binPath, 'EAMonitor.sqlite')

Initialize-EAMonitor -SqliteFilePath $SqliteFile -CreateDb

Register-EAMonitor -Path 'C:\Users\Ryan2\OneDrive\Code\EAMonitor\src\FakeMonitor.tests.ps1'

#$context = New-EFPoshContext -SQLiteFile  -AssemblyFile "C:\Users\Ryan2\OneDrive\Code\EAMonitor\src\EAMonitor\Dependencies\net6.0\EAMonitorDb.dll" -ClassName 'EAMonitorContextSqlite' -EnsureCreated
