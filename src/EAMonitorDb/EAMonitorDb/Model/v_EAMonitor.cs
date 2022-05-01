using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor
{
    public class v_EAMonitor
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Created { get; set; }
        public string MonitorState { get; set; }
        public DateTime? LastJobCreatedAt { get; set; }
        public DateTime? LastJobModifiedAt { get; set; }
        public DateTime? LastJobCompletedAt { get; set; }
        public string LastJobStatus { get; set; }

    }
}
