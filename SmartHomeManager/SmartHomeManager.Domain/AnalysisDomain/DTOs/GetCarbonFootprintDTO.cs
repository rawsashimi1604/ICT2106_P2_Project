using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class GetCarbonFootprintDTO : ResponseDTO
    {
        public IEnumerable<GetCarbonFootprintDTOData> Data { get; set; }
        public ResponseInformation Response { get; set; }
    }

    public class GetCarbonFootprintDTOData : IDTOData
    {
        public Guid CarbonFootprintId { get; set; }
        public Guid AccountId { get; set; }
        public double HouseholdConsumption { get; set; }
        public double NationalHouseholdConsumption { get; set; }
        public int MonthOfAnalysis { get; set; }
        public int YearOfAnalysis { get; set; }
    }

}
