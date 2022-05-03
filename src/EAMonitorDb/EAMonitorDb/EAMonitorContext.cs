using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor
{
    public class EAMonitorContext : DbContext
    {
        public EAMonitorContext(DbContextOptions options) : base(options)
        {
            
        }
        public EAMonitorContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }
        public DbSet<EAMonitor> EAMonitor { get; set; }
        public DbSet<EAMonitorJob> EAMonitorJob { get; set; }
        public DbSet<EAMonitorJobStatus> EAMonitorJobStatus { get; set; }
        public DbSet<EAMonitorState> EAMonitorState { get; set; }
        public DbSet<EAMonitorSettingKey> EAMonitorSettingKey { get; set; }
        public DbSet<EAMonitorSetting> EAMonitorSetting { get; set; }
        public DbSet<EAMonitorJobTest> EAMonitorJobTest { get; set; }

#if NET472
        public DbQuery<v_EAMonitor> v_EAMonitor { get; set; }
#else
        public DbSet<v_EAMonitor> v_EAMonitor { get; set; }
#endif


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EAMonitor>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<EAMonitorJobStatus>().HasData(
                new EAMonitorJobStatus { Name = "Created", Id = 1 },
                new EAMonitorJobStatus { Name = "InProgress", Id = 2 },
                new EAMonitorJobStatus { Name = "Completed", Id = 3 },
                new EAMonitorJobStatus { Name = "Failed", Id = 4 },
                new EAMonitorJobStatus { Name = "Cancelled", Id = 5 }
            );
            /*modelBuilder.Entity<EAMonitorSettingKey>().HasData(
                new EAMonitorSettingKey { Name = "Schedule-RunTimespan", Id = 1 },
                new EAMonitorSettingKey { Name = "Notify-Emails", Id = 2 },
                new EAMonitorSettingKey { Name = "Notify-Repeat", Id = 3 },
                new EAMonitorSettingKey { Name = "Enabled", Id = 4 },
                new EAMonitorSettingKey { Name = "Schedule-CRONExpression", Id = 5 },
                new EAMonitorSettingKey { Name = "Tags", Id = 6 },
                new EAMonitorSettingKey { Name = "StateChange-UpToDown", Id = 7 },
                new EAMonitorSettingKey { Name = "StateChange-UpToWarning", Id = 8 },
                new EAMonitorSettingKey { Name = "StateChange-DownToWarning", Id = 9 },
                new EAMonitorSettingKey { Name = "StateChange-DownToUp", Id = 10 }
            );*/
            modelBuilder.Entity<EAMonitorState>().HasData(
                new EAMonitorState { Name = "Unknown", Id = 1 },
                new EAMonitorState { Name = "Up", Id = 2 },
                new EAMonitorState { Name = "Down", Id = 3 },
                new EAMonitorState { Name = "Warning", Id = 4 }
            );
            modelBuilder.Entity<EAMonitorJob>().HasIndex(p => new { p.MonitorId, p.Created });
#if NET472
            modelBuilder.Query<v_EAMonitor>().ToView("v_EAMonitor");
#else
            modelBuilder.Entity<v_EAMonitor>().ToView("v_EAMonitor").HasKey(p => p.Id);
#endif
            
    }
}
    public class EAMonitorContextSQL : EAMonitorContext
    {
        private string _conString;
        public EAMonitorContextSQL()
        {
            _conString = Environment.GetEnvironmentVariable("EAMonitor_SQLConnectionString");
        }
        public EAMonitorContextSQL(string connectionString)
        {
            _conString = connectionString;
        }
        public EAMonitorContextSQL(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_conString);
                
            }
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class EAMonitorContextSqlite : EAMonitorContext
    {
        private string _conString;
        public EAMonitorContextSqlite()
        {
            _conString = Environment.GetEnvironmentVariable("EAMonitor_SQLiteConnectionString");
        }
        public EAMonitorContextSqlite(string connectionString)
        {
            _conString = connectionString;
        }
        public EAMonitorContextSqlite(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_conString);
                
            }
            base.OnConfiguring(optionsBuilder);

        }
    }

    public class EAMonitorContextSQLNet47 : EAMonitorContextSQL
    {
        private string _conString;
        public EAMonitorContextSQLNet47()
        {
            _conString = Environment.GetEnvironmentVariable("EAMonitor_SQLConnectionString");
        }
        public EAMonitorContextSQLNet47(string connectionString)
        {
            _conString = connectionString;
        }
        public EAMonitorContextSQLNet47(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_conString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }

    public class EAMonitorContextSqliteNet47 : EAMonitorContextSqlite
    {
        private string _conString;
        public EAMonitorContextSqliteNet47()
        {
            _conString = Environment.GetEnvironmentVariable("EAMonitor_SQLiteConnectionString");
        }
        public EAMonitorContextSqliteNet47(string connectionString)
        {
            _conString = connectionString;
        }
        public EAMonitorContextSqliteNet47(DbContextOptions options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(_conString);
            }
            base.OnConfiguring(optionsBuilder);
        }
    }
}
