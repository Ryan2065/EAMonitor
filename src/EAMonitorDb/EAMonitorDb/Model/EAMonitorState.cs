using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAMonitor
{
    public class EAMonitorState
    {
        public EAMonitorState()
        {
            Monitors = new HashSet<EAMonitor>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [ForeignKey("MonitorStateId")]
        public ICollection<EAMonitor> Monitors { get; set; }

    }
}
