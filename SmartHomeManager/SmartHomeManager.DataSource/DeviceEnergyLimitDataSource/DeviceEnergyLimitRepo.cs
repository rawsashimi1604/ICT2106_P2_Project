using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceEnergyLimit.Entities;
using SmartHomeManager.Domain.DeviceEnergyLimit.Interfaces;
using SmartHomeManager.Domain.EnergyProfileDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceEnergyLimitDataSource
{
    public class DeviceEnergyLimitRepository : IDeviceEnergyLimitRepository
    {

        private readonly ApplicationDbContext _db;
        private readonly DbSet<DeviceEnergyLimit> _dbSet;

        public DeviceEnergyLimitRepository(ApplicationDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<DeviceEnergyLimit>();
        }

        public async Task<DeviceEnergyLimit?> GetDeviceEnergyLimitById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(DeviceEnergyLimit deviceEnergyLimit)
        {
            _dbSet.Update(deviceEnergyLimit);
        }
        //public async Task<IEnumerable<DeviceEnergyLimit>> GetAllDeviceEnergyLimit()
        //{
        //    // get all accounts
        //    return await _dbSet.DeviceEnergyLimit.ToListAsync();
        //}

        public async Task<DeviceEnergyLimit?> Get(Guid id)
        {
            var result = await _dbSet.FindAsync(id);

            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }


        public async Task<IEnumerable<DeviceEnergyLimit>> GetAll()
        {
            IEnumerable<DeviceEnergyLimit> query = await _dbSet.ToListAsync();
            return query;
        }


    }
}
