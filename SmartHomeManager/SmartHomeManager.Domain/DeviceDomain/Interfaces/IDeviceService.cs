using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
    public interface IDeviceService
    {
        public Task<IEnumerable<Device>> GetAllDevicesByAccount(Guid accountId);
        public Task<Device> GetDeviceById(Guid deviceId);
    }
}
