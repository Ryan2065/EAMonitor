using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor
{
    public class EAMonitorJobTest
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Job")]
        public Guid JobId { get; set; }
        public EAMonitorJob Job { get; set; }
        public string TestPath { get; set; }
        public string TestExpandedPath { get; set; }
        public string Data { get; set; }
        public bool Passed { get; set; }

        public DateTime? ExecutedAt { get; set; }
    }
}
