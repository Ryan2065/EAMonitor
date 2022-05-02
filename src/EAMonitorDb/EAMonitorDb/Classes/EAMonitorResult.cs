using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;


namespace EAMonitor.Classes
{
    public class EAMonitorResult
    {
        public EAMonitorResult()
        {
            
        }
        public EAMonitorRegistered Monitor { get; set; }
        public EAMonitorJob Job { get; set; }
        public object TestResult { get; set; }
        public object Data { get; set; }
    }
}
