using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor.Classes
{
    public class EAMonitorCacheData
    {
        public DateTime ExpireAt { get; set; }
        public object CachedResult { get; set; }
    }
}
