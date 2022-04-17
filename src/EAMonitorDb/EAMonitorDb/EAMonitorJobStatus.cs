using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EAMonitorDb
{
    public class EAMonitorJobStatus
    {
        public EAMonitorJobStatus()
        {
            Jobs = new HashSet<EAMonitorJob>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [ForeignKey("JobStatusId")]
        public ICollection<EAMonitorJob> Jobs { get; set; }
    }
}
