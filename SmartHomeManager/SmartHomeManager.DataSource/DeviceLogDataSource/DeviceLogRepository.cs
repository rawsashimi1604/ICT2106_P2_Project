using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.DataSource.DeviceLogDataSource
{
    public class DeviceLogRepository: IDeviceLogRepository
    {
        private readonly ApplicationDbContext _db;
        private DbSet<DeviceLog> _dbSet;

        public DeviceLogRepository(ApplicationDbContext db) {
            _db = db;
            this._dbSet = db.Set<DeviceLog>();
        }

       

        public IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date)
        {
            // get all logs
            var allLogs = _db.DeviceLogs.ToList();

            IEnumerable<DeviceLog> result = _db.DeviceLogs.ToList().Where(log => log.DeviceId == deviceId && log.DateLogged.Date == date.Date);

            return result;
        }

        public async Task<IEnumerable<DeviceLog>> GetAllByDeviceId(Guid deviceId)
        {
            var allLogs = _db.DeviceLogs.ToList();

            IEnumerable<DeviceLog> result = allLogs.Where(log => log.DeviceId == deviceId);

            return result;
        }

        public IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date, DateTime endTime)
        {
            // get all logs
            var allLogs = _db.DeviceLogs.ToList();

            IEnumerable<DeviceLog> result = _db.DeviceLogs.ToList().Where(log => log.DeviceId == deviceId && log.DateLogged.Date == date && log.DateLogged.TimeOfDay >= date.TimeOfDay && log.EndTime?.TimeOfDay <= endTime.TimeOfDay) ;

            return result;
        }

        // there should only be 1 device log where device state is True everyday
        public async Task<DeviceLog?> Get(DateTime date, Guid deviceId, bool deviceState)
        {
            var result = await _dbSet.FindAsync(date, deviceId, deviceState);
            return result;
        }

        public async Task<DeviceLog?> Get(DateTime date)
        {
            var result = await _dbSet.FindAsync(date); 
            return result;
        }

        public async Task<IEnumerable<DeviceLog>> GetAll()
        {
            IEnumerable<DeviceLog> query = await _dbSet.ToListAsync();
            return query;
        }

        public async Task<DeviceLog> GetByDate(DateTime date, Guid deviceId, bool deviceState)
        {
            var result = await _dbSet.FindAsync(date, deviceId, deviceState);
            return result;
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public void Add(DeviceLog entity)
        {
            _dbSet.Add(entity);
        }
        public void Update(DeviceLog entity)
        {
            _dbSet.Update(entity);
        }

        
    }
}
