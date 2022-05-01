Function Initialize-EAMonitor{
    [CmdletBinding()]
    Param(
        [Parameter(Mandatory = $true, ParameterSetName = "Sqlite")]
        [string]$SqliteFilePath,
        [Parameter(Mandatory = $true, ParameterSetName = "Sql")]
        [string]$SqlServer,
        [Parameter(Mandatory = $true, ParameterSetName = "Sql")]
        [string]$Database,
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [bool]$IntegratedSecurity = $false,
        [Parameter(Mandatory = $true, ParameterSetName = "ConnectionString")]
        [string]$ConnectionString,
        [Parameter(Mandatory = $true, ParameterSetName = "ConnectionString")]
        [ValidateSet('MSSQL', 'SQLite')]
        [string]$DatabaseType,
        [Parameter(Mandatory = $false, ParameterSetName = "ConnectionString")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sqlite")]
        [switch]$DetectMonitorModules,
        [Parameter(Mandatory = $false, ParameterSetName = "ConnectionString")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sqlite")]
        [switch]$CreateDb,
        [Parameter(Mandatory = $false, ParameterSetName = "ConnectionString")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sql")]
        [Parameter(Mandatory = $false, ParameterSetName = "Sqlite")]
        [string]$Environment
    )
    begin{
        $Script:efPoshDbContextParams = @{
            'EnsureCreated' = $true -eq $CreateDb
            'RunMigrations' = $true -eq $CreateDb
        }
        $ParentDirectory = (Get-Item $PSScriptRoot).Parent
        $ModulesToImport = @()
    }
    Process{
        if($PSVersionTable.PSVersion.Major -gt 5){
            $Script:efPoshDbContextParams['AssemblyFile'] = [System.IO.Path]::Combine($ParentDirectory.FullName, "Dependencies", "net6.0", "EAMonitor.dll")
        }
        else{
            $Script:efPoshDbContextParams['AssemblyFile'] = [System.IO.Path]::Combine($ParentDirectory.FullName, "Dependencies", "net472", "EAMonitor.dll")
        }

        if($Environment){
            $Script:EAMonitorEnvironment = $Environment
        }
        
        if($PSCmdlet.ParameterSetName -eq 'Sqlite'){
            $Script:efPoshDbContextParams['SQLiteFile'] = $SqliteFilePath
            $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqlite'
        }
        elseif($PSCmdlet.ParameterSetName -eq 'ConnectionString'){
            $Script:efPoshDbContextParams['DBType'] = $DatabaseType
            $Script:efPoshDbContextParams['ConnectionString'] = $ConnectionString
            if($DatabaseType -eq 'SQLite'){
                $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSqlite'
            }
            elseif($DatabaseType -eq 'MSSQL'){
                $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSQL'
            }
        }
        elseif($PSCmdlet.ParameterSetName -eq 'Sql'){
            $Script:efPoshDbContextParams['MSSQLServer'] = $SqlServer
            $Script:efPoshDbContextParams['MSSQLDatabase'] = $Database
            $Script:efPoshDbContextParams['MSSQLIntegratedSecurity'] = $IntegratedSecurity
            $Script:efPoshDbContextParams['ClassName'] = 'EAMonitorContextSQL'
        }
        else{
            throw "Congrats - this should be impossible! Open an issue and provide the parameters used to call this function"
            return
        }
        New-EAMonitorDbContext -Force

        foreach($module in $ModulesToImport){
            Write-Verbose "Importing module $($module.Name)"
            Import-Module $module.Name -Force -Verbose:$false
        }
    }
    end{
        if($DetectMonitorModules){
            $ModulesToImport = Get-Module -Name 'EAMonitor-*' -ListAvailable
            foreach($module in $ModulesToImport){
                Import-Module $Module.Name -Force
            }
        }
    }
}