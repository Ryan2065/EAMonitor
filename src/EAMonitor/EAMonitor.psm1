$Module = Import-Module -Name Pester -MinimumVersion 5.3.0 -PassThru -ErrorAction SilentlyContinue -Verbose:$false
if($null -eq $Module){
    Write-Warning "wfMonitor was designed for Pester 5.3 and above. It was not found. Please install Pester 5.3.0 or higher.`nWill continue, but results may be incomplete."
    Import-Module -Name Pester -Verbose:$false
}

$CommandFolder = [System.IO.Path]::Combine($PSScriptRoot, 'Commands')
$PrivateCommandFolder = [System.IO.Path]::Combine($PSScriptRoot, 'PrivateCommands')

foreach($classFile in Get-ChildItem $PrivateCommandFolder -Filter '*.ps1'){
    . $classFile.FullName
}

$Commands = Get-ChildItem $CommandFolder -Filter '*.ps1'
foreach($classFile in $Commands){
    . $classFile.FullName
}


Export-ModuleMember -Function $Commands.BaseName