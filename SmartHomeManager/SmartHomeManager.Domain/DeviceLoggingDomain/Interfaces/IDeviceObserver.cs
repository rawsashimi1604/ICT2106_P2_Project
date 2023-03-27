using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{

    public interface IDeviceObserver
    {
        // bfe log repo have to state the methods needed from ds
        IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date);
        IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date, DateTime endTime);

        void Update(Device deviceState);
    }
}
