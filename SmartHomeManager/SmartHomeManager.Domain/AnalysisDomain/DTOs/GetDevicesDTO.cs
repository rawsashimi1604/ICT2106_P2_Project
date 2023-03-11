using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class GetDevicesDTO : ResponseDTO
    {
        public IEnumerable<GetDevicesDTOData> Data { get; set; }
        public ResponseInformation Response { get; set; }
    }

    public class GetDevicesDTOData : IDTOData
    {
        public Guid DeviceID { get; set; }
        public string DeviceName { get; set; }
    }
}
