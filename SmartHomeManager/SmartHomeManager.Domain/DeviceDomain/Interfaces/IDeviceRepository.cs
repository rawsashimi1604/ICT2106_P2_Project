using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
    public interface IDeviceRepository
    {
        public Task AddAsync(Device device);

        public Task<Device> GetAsync(Guid deviceId);

        public Task<IEnumerable<Device>> GetAllAsync();

        public Task UpdateAsync(Device device);

        public Task DeleteAsync(Guid deviceId);

        public Task<bool> SaveAsync();
    }
}

