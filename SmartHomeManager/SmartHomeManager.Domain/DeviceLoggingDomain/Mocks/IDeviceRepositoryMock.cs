using SmartHomeManager.Domain.DeviceDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Mocks
{
    public interface IDeviceRepositoryMock
    {
        IEnumerable<Device> GetAllDevicesInRoom(Guid roomId);

    }
}
