using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{   
    // To be passed into Team5 - Analysis.
    public interface IDeviceInfoService
    {
        public Task<IEnumerable<DeviceLog>> GetAllDeviceLogAsync();

        public Task<IEnumerable<DeviceLog>> GetDeviceLogByIdAsync(Guid deviceId);

    }
}
