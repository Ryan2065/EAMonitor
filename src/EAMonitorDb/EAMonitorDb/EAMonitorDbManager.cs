using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor
{
    public enum EAMonitorDbType
    {
        Sql,
        Sqlite,
        SqlNet47,
        SqliteNet47
    }
    public static class EAMonitorDbManager
    {
        public static EAMonitorContext GetDbContext(string connectionString, EAMonitorDbType dbType, bool EnsureCreated, bool ApplyMigrations)
        {
            EAMonitorContext dbContext = null;
            switch (dbType)
            {
                case EAMonitorDbType.Sql:
                    dbContext = new EAMonitorContextSQL(connectionString);
                    break;
                case EAMonitorDbType.Sqlite:
                    dbContext = new EAMonitorContextSqlite(connectionString);
                    break;
                case EAMonitorDbType.SqlNet47:
                    dbContext = new EAMonitorContextSQLNet47(connectionString);
                    break;
                case EAMonitorDbType.SqliteNet47:
                    dbContext = new EAMonitorContextSqliteNet47(connectionString);
                    break;
            }

            if (EnsureCreated)
            {
                dbContext.Database.EnsureCreated();
            }
            if (ApplyMigrations)
            {
                dbContext.Database.Migrate();
            }
            return dbContext;
        }
    }
}
