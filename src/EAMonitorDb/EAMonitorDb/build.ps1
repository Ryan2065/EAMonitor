
Push-Location $PSScriptRoot

dotnet publish --self-contained false --configuration release --framework net472 
dotnet publish --self-contained false --configuration release --framework net6.0  

$RuntimeFoldersToKeep = @(
    'win-x64',
    'win-x86',
    'linux-x64',
    'osx'
)
$RuntimeFolders = Get-ChildItem "$PSScriptRoot\bin\release\net6.0\publish\runtimes" -Directory -ErrorAction SilentlyContinue
foreach($RuntimeFolder in $RuntimeFolders){
    if($RuntimeFoldersToKeep -notcontains $RuntimeFolder.BaseName){
        #Remove-Item $RuntimeFolder.FullName -Recurse -Force
    }
}

Pop-Location
