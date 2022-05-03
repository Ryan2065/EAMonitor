$Module = Import-Module -Name Pester -MinimumVersion 5.3.0 -PassThru -ErrorAction SilentlyContinue -Verbose:$false

if($null -eq $Module){
    Write-Warning "wfMonitor was designed for Pester 5.3 and above. It was not found. Please install Pester 5.3.0 or higher.`nWill continue, but results may be incomplete."
    Import-Module -Name Pester -Verbose:$false
}

Import-Module EAMemoryCache -ErrorAction Stop
Import-Module EFPosh -ErrorAction Stop

$CommandFolder = [System.IO.Path]::Combine($PSScriptRoot, 'Commands')
$PrivateCommandFolder = [System.IO.Path]::Combine($PSScriptRoot, 'PrivateCommands')

foreach($privCommand in Get-ChildItem $PrivateCommandFolder -Filter '*.ps1'){
    . $privCommand.FullName
}

$Commands = Get-ChildItem $CommandFolder -Filter '*.ps1'
foreach($cmdFile in $Commands){
    . $cmdFile.FullName
}

# Some of the module might not work correctly if the EFPosh dlls and EAMonitor dlls aren't loaded. They are loaded when the 
# DbContext is created. Since EnsureCreated and RunMigrations are false, this will just create the context object in memory
# but will not actually create the Db. That will happen on Initialize when we know the real DB information from the user.
$Script:efPoshDbContextParams = @{
    'EnsureCreated' = $false
    'RunMigrations' = $false
}
if($PSVersionTable.PSVersion.Major -gt 5){
    $Script:efPoshDbContextParams['AssemblyFile'] = [System.IO.Path]::Combine($PSScriptRoot, "Dependencies", "net6.0", "EAMonitor.dll")
    $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqlite'
}
else{
    $Script:efPoshDbContextParams['AssemblyFile'] = [System.IO.Path]::Combine($PSScriptRoot, "Dependencies", "net472", "EAMonitor.dll")
    $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqliteNet47'
}
$Script:efPoshDbContextParams['SQLiteFile'] = [System.IO.Path]::Combine($env:Temp, "EAMonitor.sqlite")
New-EAMonitorDbContext -Force

Export-ModuleMember -Function $Commands.BaseName