using SmartHomeManager.API.Controllers.NotificationAPIs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.AnalysisDomain.DTOs
{
    public class GetDevicesDTO
    {
        public List<GetDevicesObjectDTO> DevicesObject { get; set; }
        public ResponseObjectDTO ResponseObject { get; set; }
    }

    public class GetDevicesObjectDTO
    {
        public Guid DeviceID { get; set; }  
    }
}
