using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class ForecastChartDataDTO
    {
        public List<ForecastChartDataObjectDTO>? ForecastChartDataObject { get; set; }
    }
    public class ForecastChartDataObjectDTO
    {
        public Guid ForecastChartDataId { get; set; }
        public Guid ForcastChartId { get; set; }
        public string? Label { get; set; }
        public double Value { get; set; }
        public bool IsForecast { get; set; }
        public int Index { get; set; }
    }
}
