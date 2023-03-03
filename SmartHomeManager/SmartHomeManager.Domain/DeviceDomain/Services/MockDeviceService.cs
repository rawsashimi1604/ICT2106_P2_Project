using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceDomain.Services
{
    public class MockDeviceService

    {
        private readonly IDeviceRepository _deviceRepository;

        public MockDeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<IEnumerable<Device>> GetAllDevicesByAccount(Guid accountId)
        {
            IEnumerable<Device> devices = await _deviceRepository.GetAllAsyncByAccountId(accountId);
            return devices;
        }

        public async Task<Device> GetDeviceById(Guid deviceId)
        {
            Device device = await _deviceRepository.GetAsync(deviceId);
            return device;
        }

    }
}
