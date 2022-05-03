using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor.Classes
{
    public class EAMonitorSettingObject
    {
        public EAMonitorSettingObject(string key, string value, EAMonitorSettingLocation valueFrom, string valueFromData = "", string description = null, string forMonitor = null)
        {
            Key = key;
            Value = value;
            ValueFrom = valueFrom;
            ValueFromData = valueFromData;
            Description = description;
            ForMonitor = forMonitor;
        }
        public string Key { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string ForMonitor { get; set; }
        public EAMonitorSettingLocation ValueFrom { get; set; }
        public string ValueFromData { get; set; }
    }
    public enum EAMonitorSettingLocation
    {
        SqlDefault,
        SqlMonitor,
        MonitorFile,
        MonitorEnvironmentFile
    }
}
