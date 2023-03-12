using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AnalysisDomain.Entities
{
    public class CarbonFootprint
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CarbonFootprintId { get; set; }
        [Required]
        public Guid AccountId { get; set; }
        [Required]
        public double HouseholdConsumption { get; set; }
        [Required]
        public double NationalHouseholdConsumption { get; set; }
        [Required]
        public int MonthOfAnalysis { get; set; }
        [Required]
        public int YearOfAnalysis { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
