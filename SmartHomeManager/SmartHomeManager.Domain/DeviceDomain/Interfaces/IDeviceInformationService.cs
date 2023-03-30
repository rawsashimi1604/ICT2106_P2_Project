using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
    public interface IDeviceInformationService
    {
        public Task<IEnumerable<Device>> GetAllDevicesByAccountAsync(Guid accountId);
        public Task<Device?> GetDeviceByIdAsync(Guid deviceId);

        public Task<bool> IsDeviceOnAsync(Guid deviceId);

        public Task<IEnumerable<Device>> GetDevicesInRoomAsync(Guid roomId);
    }
}
