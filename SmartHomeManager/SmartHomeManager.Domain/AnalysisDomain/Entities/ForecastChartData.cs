using SmartHomeManager.Domain.AccountDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.Entities
{
    public class ForecastChartData
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ForecastChartDataId { get; set; }

        [Required]
        public Guid ForecastChartId { get; set; }

        [Required]
        public string Label { get; set; }

        [Required]
        public double Value { get; set; }
        [Required]
        public bool IsForecast { get; set; }

        [Required]
        public int Index { get; set; }

        [ForeignKey("ForecastChartId")]
        public ForecastChart ForecastChart { get; set; }

        /*public static implicit operator forecastchartdata(forecastchartdata v)
        {
            throw new notimplementedexception();
        }*/
    }
}
