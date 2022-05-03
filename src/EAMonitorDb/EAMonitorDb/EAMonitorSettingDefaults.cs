using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor
{
    internal static class EAMonitorSettingDefaults
    {
        internal static int KeyCount = 0;
        internal static int SettingCount = 0;
        internal static void AddEAMonitorSettingKeyDefault(this ModelBuilder modelBuilder, string name, string Description, string defaultValue = "")
        {
            KeyCount++;
            modelBuilder.Entity<EAMonitorSettingKey>().HasData(
                new EAMonitorSettingKey
                {
                    Id = KeyCount,
                    Description = "Monitor description",
                    Name = "Description"
                }
            );
            if (!String.IsNullOrEmpty(defaultValue))
            {
                SettingCount++;
                modelBuilder.Entity<EAMonitorSetting>().HasData(
                    new EAMonitorSetting
                    {
                         Id = SettingCount,
                         LastModified = DateTime.UtcNow,
                         SettingKeyId = KeyCount,
                         SettingValue = defaultValue
                    }
                );
            }
        }
        internal static void AddEAMonitorSettingKeyReserve(this ModelBuilder modelBuilder)
        {
            while(KeyCount < 200)
            {
                KeyCount++;
                modelBuilder.Entity<EAMonitorSettingKey>().HasData(
                    new EAMonitorSettingKey
                    {
                        Id = KeyCount,
                        Description = "Reserved setting key for future features",
                        Name = $"__Reserved{KeyCount}__"
                    }
                );
                SettingCount++;
                modelBuilder.Entity<EAMonitorSetting>().HasData(
                    new EAMonitorSetting
                    {
                        Id = SettingCount,
                        LastModified = DateTime.UtcNow,
                        SettingKeyId = KeyCount,
                        SettingValue = $"__Reserved{KeyCount}__"
                    }
                );
            }
        }
    }
}
