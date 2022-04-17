Param($MigrationName = "Initial", $RemovePrevious = $true)

Push-Location $PSScriptRoot

[System.Environment]::SetEnvironmentVariable("EAMonitor_SQLConnectionString", "Server=Lab-CM.Home.Lab;Database=EAMonitor;Trusted_Connection=True;")
[System.Environment]::SetEnvironmentVariable("EAMonitor_SQLiteConnectionString", "Data Source=SQLDb.sqlite;")

if($RemovePrevious){
    #If only one migration, can just nuke the migration folder
    try{
        $NumberOfMigrations = ((Get-ChildItem "$PSScriptRoot\Migrations\Sql" -Filter "*.cs").Count - 1) / 2
        if($NumberOfMigrations -eq 1){
            $null = Remove-Item "$PSScriptRoot\Migrations" -Force -Recurse
        }
    }
    catch{
        throw
    }
    if(Test-Path "$PSScriptRoot\Migrations\SQL\EAMonitorContextSQLModelSnapshot.cs"){
        dotnet build --runtime net6.0 --no-self-contained
        dotnet ef migrations remove --no-build --context "EAMonitorContextSQL" --runtime net6.0
        dotnet ef migrations remove --no-build --context "EAMonitorContextSqlite" --runtime net6.0
        
        dotnet build --runtime net472 --no-self-contained
        dotnet ef migrations remove --no-build --context "EAMonitorContextSQLNet47" --runtime net472
        dotnet ef migrations remove --no-build --context "EAMonitorContextSqliteNet47" --runtime net472
    }
}

dotnet build --runtime net6.0 --no-self-contained
dotnet ef migrations add "$MigrationName" --no-build --context "EAMonitorContextSQL" --output-dir "Migrations/SQL" --runtime net6.0
dotnet ef migrations add "$MigrationName" --no-build --context "EAMonitorContextSqlite" --output-dir "Migrations/SQLite" --runtime net6.0
dotnet build --runtime net472 --no-self-contained
dotnet ef migrations add "$MigrationName" --no-build --context "EAMonitorContextSQLNet47" --output-dir "Migrations/SQLNet47" --runtime net472
dotnet ef migrations add "$MigrationName" --no-build --context "EAMonitorContextSqliteNet47" --output-dir "Migrations/SQLiteNet47" --runtime net472

$BasePath = "C:\Users\Ryan2\OneDrive\Code\EAMonitor\src\EAMonitorDb\EAMonitorDb"
$SqlMigrationName = (Get-ChildItem "$BasePath\Migrations\Sql" -Filter "*_$($MigrationName).cs").BaseName
$SqliteMigrationName = (Get-ChildItem "$BasePath\Migrations\Sqlite" -Filter "*_$($MigrationName).cs").BaseName
$SqlNet47MigrationName = (Get-ChildItem "$BasePath\Migrations\SqlNet47" -Filter "*_$($MigrationName).cs").BaseName
$SqliteNet47MigrationName = (Get-ChildItem "$BasePath\Migrations\SqliteNet47" -Filter "*_$($MigrationName).cs").BaseName

(Get-Content "$BasePath\Migrations\SqlNet47\$($SqlNet47MigrationName).Designer.cs" -Raw).Replace($SqlNet47MigrationName, $SqlMigrationName) | Out-File "$BasePath\Migrations\SqlNet47\$($SqlNet47MigrationName).Designer.cs" -Force
Rename-Item "$BasePath\Migrations\SqlNet47\$($SqlNet47MigrationName).Designer.cs" -NewName "$($SqlMigrationName).Designer.cs" -Force
Rename-Item "$BasePath\Migrations\SqlNet47\$($SqlNet47MigrationName).cs" -NewName "$($SqlMigrationName).cs"

(Get-Content "$BasePath\Migrations\SqliteNet47\$($SqliteNet47MigrationName).Designer.cs" -Raw).Replace($SqliteNet47MigrationName, $SqliteMigrationName) | Out-File "$BasePath\Migrations\SqliteNet47\$($SqliteNet47MigrationName).Designer.cs" -Force
Rename-Item "$BasePath\Migrations\SqliteNet47\$($SqliteNet47MigrationName).Designer.cs" -NewName "$($SqliteMigrationName).Designer.cs" -Force
Rename-Item "$BasePath\Migrations\SqliteNet47\$($SqliteNet47MigrationName).cs" -NewName "$($SqliteMigrationName).cs"

Pop-Location