using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAMonitor
{
    public class EAMonitorJob
    {
        public EAMonitorJob()
        {
            Tests = new HashSet<EAMonitorJobTest>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("Monitor")]
        public Guid MonitorId { get; set; }
        public EAMonitor Monitor { get; set; }

        [ForeignKey("JobStatus")]
        [Required]
        public int JobStatusId { get; set; }
        public EAMonitorJobStatus JobStatus { get; set; }

        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime? Completed { get; set; }

        public bool Notified { get; set; }


        [ForeignKey("JobId")]
        public ICollection<EAMonitorJobTest> Tests { get; set; }


    }
}
