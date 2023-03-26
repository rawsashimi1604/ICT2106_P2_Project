using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.Common.DTOs;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class GetDeviceReportDTO : ResponseDTO
    {
        public IEnumerable<object> Data { get; set; }
        public ResponseInformation Response { get; set; }
    }
}
