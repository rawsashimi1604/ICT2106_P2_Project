using SmartHomeManager.DataSource.Interfaces;
using SmartHomeManager.Domain.DeviceEnergyLimit.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartHomeManager.DataSource.DeviceEnergyLimitDataSource
{
    public class DeviceEnergyLimitRepository : IDeviceEnergyLimitRepository
    {

        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<DeviceEnergyLimit> _dbSet;

        public DeviceEnergyLimitRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<DeviceEnergyLimit>();
        }

        public async Task<DeviceEnergyLimit?> GetDeviceEnergyLimitById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(DeviceEnergyLimit deviceEnergyLimit)
        {
            _dbSet.Update(deviceEnergyLimit);
        }

        public async Task<IEnumerable<DeviceEnergyLimit>> GetAllDeviceEnergyLimit()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<DeviceEnergyLimit?> Get(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<DeviceEnergyLimit>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<DeviceLog> Get(Guid deviceId, DateTime dateTime)
        {
            return await _dbContext.DeviceLogs.Where(d => d.DeviceId == deviceId && d.DateTime == dateTime).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DeviceLog>> Get(Guid deviceId, DateTime startDateTime, DateTime endDateTime)
        {
            return await _dbContext.DeviceLogs.Where(d => d.DeviceId == deviceId && d.DateTime >= startDateTime && d.DateTime <= endDateTime).ToListAsync();
        }

        public async Task<IEnumerable<DeviceLog>> GetByDate(DateTime dateTime, Guid deviceId, bool deviceState)
        {
            return await _dbContext.DeviceLogs.Where(d => d.DeviceId == deviceId && d.DateTime == dateTime && d.DeviceState == deviceState).ToListAsync();
        }

        public async Task<IEnumerable<DeviceLog>> Get(DateTime dateTime, Guid deviceId, bool deviceState)
        {
            return await _dbContext.DeviceLogs.Where(d => d.DeviceId == deviceId && d.DateTime == dateTime && d.DeviceState == deviceState).ToListAsync();
        }

        public async Task<IEnumerable<DeviceLog>> Get(DateTime dateTime)
        {
            return await _dbContext.DeviceLogs.Where(d => d.DateTime == dateTime).ToListAsync();
        }

        public async Task Add(DeviceLog entity)
        {
            await _dbContext.DeviceLogs.AddAsync(entity);
        }
    }
}
