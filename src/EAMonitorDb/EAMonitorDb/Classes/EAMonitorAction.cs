using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor.Classes
{
    public class EAMonitorAction
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public ScriptBlock Script { get; set; }
    }
}
