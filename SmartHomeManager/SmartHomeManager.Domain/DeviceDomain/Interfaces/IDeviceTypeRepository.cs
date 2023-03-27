using System;
using SmartHomeManager.Domain.DeviceDomain.Entities;

namespace SmartHomeManager.Domain.DeviceDomain.Interfaces
{
    public interface IDeviceTypeRepository
    {
        public Task AddAsync(DeviceType deviceType);

        public Task<DeviceType> GetAsync(string deviceType);

        public Task<IEnumerable<DeviceType>> GetAllAsync();

        public Task UpdateAsync(DeviceType deviceType);

        public Task DeleteAsync(string deviceTypeName);

        public Task<bool> SaveAsync();
    }
}

