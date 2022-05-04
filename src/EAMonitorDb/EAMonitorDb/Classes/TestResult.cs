using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor.Classes
{
    public class TestResult
    {
        public string Name { get; set; }
        public string ExpandedName { get; set; }
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public List<string> Path { get; set; }
        public string ExpandedPath { get; set; }
        public List<object> ErrorRecord { get; set; } = new List<object>();
        public bool Passed { get; set; }
        public string MonitorName { get; set; }
        public Dictionary<string, string> MonitorSettings { get; set; }
        public EAMonitorJob PreviousJob { get; set; }

    }
}
