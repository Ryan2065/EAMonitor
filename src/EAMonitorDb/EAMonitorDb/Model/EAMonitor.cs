using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EAMonitor
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

        public DateTime LastModified { get; set; }

        public DateTime Created { get; set; }

        public int MonitorStateId { get; set; }
        public EAMonitorState MonitorState { get; set; }

        public ICollection<EAMonitorJob> Jobs { get; set; }

        public ICollection<EAMonitorSetting> Settings { get; set; }

    }
}