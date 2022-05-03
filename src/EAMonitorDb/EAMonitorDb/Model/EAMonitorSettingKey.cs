using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EAMonitor
{
    public class EAMonitorSettingKey
    {
        public EAMonitorSettingKey()
        {
            Settings = new HashSet<EAMonitorSetting>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("SettingKeyId")]
        public ICollection<EAMonitorSetting> Settings { get; set; }
    }
}
