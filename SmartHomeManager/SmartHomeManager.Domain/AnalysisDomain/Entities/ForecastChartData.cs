using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common;

namespace SmartHomeManager.Domain.AnalysisDomain.Entities
{
    public class ForecastChartData : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ForecastChartDataId { get; set; }

        [Required]
        public Guid AccountId { get; set; }

        [Required]
        public int TimespanType { get; set; }

        [Required]
        public string DateOfAnalysis { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public double WattsValue { get; set; }

        [Required]
        public double PriceValue { get; set; }

        [Required]
        public int Index { get; set; }
    }
}
