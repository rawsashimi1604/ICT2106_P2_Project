using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class GetEnergyEfficiencyDTO : ResponseDTO
    {
        public IEnumerable<GetEnergyEfficiencyDTOData> Data { get; set; }
        public ResponseInformation Response { get; set; }
    }

    public class GetEnergyEfficiencyDTOData : IDTOData
    {
        public Guid EnergyEfficiencyId { get; set; }
        public Guid DeviceID { get; set; }
        public double EnergyEfficiencyIndex { get; set; }
    }
}
