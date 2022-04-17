using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EAMonitorDb
{
    public class EAMonitor
    {
        public EAMonitor()
        {
            Jobs = new HashSet<EAMonitorJob>();
        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public string Description { get; set; }
        /// <summary>
        /// Next time the monitor is scheduled to run
        /// </summary>
        public DateTime? NextRun { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime Created { get; set; }

        [ForeignKey("MonitorState")]
        public int MonitorStateId { get; set; }
        public EAMonitorState MonitorState { get; set; }

        [ForeignKey("MonitorId")]
        public ICollection<EAMonitorJob> Jobs { get; set; }

        [ForeignKey("MonitorId")]
        public ICollection<EAMonitorSetting> Settings { get; set; }

    }
}