using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class GetForecastChartDataDTO : ResponseDTO
    {
        public IEnumerable<ForecastChartDataObjectDTO> Data { get; set; }

        public ResponseInformation Response { get; set; }
    }
    public class ForecastChartDataObjectDTO : IDTOData
    {
        public Guid ForecastChartDataId { get; set; }
        public Guid AccountId { get; set; }
        public int TimespanType { get; set; }
        public string? DateOfAnalysis { get; set; }
        public string? Label { get; set; }
        public double WattsValue { get; set; }
        public double PriceValue { get; set; }
        public int Index { get; set; }
    }
}
