using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.DataSource.DeviceDataSource
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DeviceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Device device)
        {
            await _applicationDbContext.AddAsync(device);
        }

        public Task DeleteAsync(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _applicationDbContext.Devices.ToListAsync();
        }

        public Task<Device> GetAsync(Guid deviceId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAsync()
        {
            return await _applicationDbContext.SaveChangesAsync() > 0;
        }

        public Task UpdateAsync(Device device)
        {
            throw new NotImplementedException();
        }
    }
}
