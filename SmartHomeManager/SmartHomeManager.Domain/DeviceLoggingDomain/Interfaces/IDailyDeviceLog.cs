using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    public interface IDailyDeviceLog
    {
        public IEnumerable<DeviceLog> getDailyDeviceLogById(Guid LogID);
    }
}
