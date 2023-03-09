using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class ForecastChartDTO
    {
        public List<ForecastChartObjectDTO>? ForecastChartObject { get; set; }
    }
    public class ForecastChartObjectDTO
    {
        public Guid ForecastChartId { get; set; }
        public Guid AccountId { get; set; }
        public int TimespanType { get; set; }
        public string? DateOfAnalysis { get; set; }
    }

}
