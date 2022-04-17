$CommandFolder = [System.IO.Path]::Combine($PSScriptRoot, 'Commands')
$ClassesFolder = [System.IO.Path]::Combine($PSScriptRoot, 'Classes')
$PrivateCommandFolder = [System.IO.Path]::Combine($PSScriptRoot, 'PrivateCommands')

foreach($classFile in Get-ChildItem $ClassesFolder -Filter '*.ps1'){
    . $classFile.FullName
}

foreach($classFile in Get-ChildItem $PrivateCommandFolder -Filter '*.ps1'){
    . $classFile.FullName
}

$Commands = Get-ChildItem $CommandFolder -Filter '*.ps1'
foreach($classFile in $Commands){
    . $classFile.FullName
}

Export-ModuleMember -Function $Commands.BaseName