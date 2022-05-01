using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;

namespace EAMonitor
{
    public class EAMonitorSetting
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("SettingKey")]
        public int SettingKeyId { get; set; }
        public EAMonitorSettingKey SettingKey { get; set; }

        [ForeignKey("Monitor")]
        public Guid? MonitorId { get; set; }
        public EAMonitor Monitor { get; set; }

        [Required]
        public string SettingValue { get; set; }

        [Required]
        public DateTime LastModified { get; set; }

    }
}
