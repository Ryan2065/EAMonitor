using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation.Language;

namespace EAMonitor.Classes
{
    public class EAMonitorRegistered
    {
        public EAMonitorRegistered(string filePath)
        {
            FilePath = filePath;
            if (System.IO.File.Exists(filePath))
            {
                Directory = System.IO.Path.GetDirectoryName(filePath);
                var fileName = System.IO.Path.GetFileName(filePath);
                Name = fileName.Replace(".monitors.ps1", "", StringComparison.OrdinalIgnoreCase);
            }
        }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string Directory { get; set; }
        public EAMonitor DbMonitorObject { get; set; }
    }

}
