using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    public interface IDeviceLogInfoService
    {
        public IEnumerable<DeviceLog> getAllDeviceLog();

        public IEnumerable<DeviceLog> getDeviceLogById(Guid deviceId);

    }
}
