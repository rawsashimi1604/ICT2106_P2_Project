using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.AnalysisDomain.Entities
{
    public class EnergyEfficiency : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid EnergyEfficiencyId { get; set; }

        [Required]
        public Guid DeviceId { get; set; }

        [Required]
        public double EnergyEfficiencyIndex { get; set; }

        [Required]
        public DateTime DateOfAnalysis { get; set; }

        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }
}
