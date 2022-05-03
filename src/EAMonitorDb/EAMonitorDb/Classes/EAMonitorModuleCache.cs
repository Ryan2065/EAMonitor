using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor.Classes
{
    /// <summary>
    /// Place to store data the module will refer to
    /// </summary>
    public static class EAMonitorModuleCache
    {
        public static List<EAMonitorRegistered> RegisteredModules { get; set; } = new List<EAMonitorRegistered>();
        public static List<EAMonitorAction> Actions { get; set; } = new List<EAMonitorAction>();

        public static void Clear()
        {
            RegisteredModules.Clear();
            Actions.Clear();
        }

    }
}
