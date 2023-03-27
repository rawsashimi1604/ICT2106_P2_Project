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

        public Task<IEnumerable<Device>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Device>> GetAllAsyncByAccountId(Guid accountId)
        {
            IEnumerable<Device> devices = await _applicationDbContext.Devices.ToListAsync();
            IEnumerable<Device> result = devices.Where(x => x.AccountId == accountId);
            return result;
        }


        public async Task<Device?> GetAsync(Guid deviceId)
        {
            return _applicationDbContext.Devices.Find(deviceId);
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
