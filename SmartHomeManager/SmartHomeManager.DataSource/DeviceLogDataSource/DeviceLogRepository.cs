using Microsoft.EntityFrameworkCore;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;


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
            var allLogs = _dbSet.ToList();

            IEnumerable<DeviceLog> result = _db.DeviceLogs.ToList().Where(log => log.DeviceId == deviceId && log.DateLogged.Date == date.Date);

            return result;
        }

        public IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date, DateTime endTime)
        {
            // get all logs
            var allLogs = _dbSet.ToList();

            IEnumerable<DeviceLog> result =  _dbSet.ToList().Where(log => log.DeviceId == deviceId && log.DateLogged.Date == date && log.DateLogged.TimeOfDay >= date.TimeOfDay && log.EndTime?.TimeOfDay <= endTime.TimeOfDay) ;

            return result;
        }

        // there should only be 1 device log where device state is True everyday
        public async Task<DeviceLog?> Get(DateTime date, Guid deviceId, bool deviceState)
        {
            var result = await _dbSet.FindAsync(deviceId);
            return result;
        }

        public async Task<DeviceLog?> Get(DateTime date)
        {
            var result = await _dbSet.FindAsync(date); 
            return result;
        }

        public async Task<DeviceLog?> Get(DateTime startTime, DateTime endTime)
        {
            var result = await _dbSet.FindAsync(startTime, endTime);
            return result;
        }

        public async Task<IEnumerable<DeviceLog>> GetAll()
        {
            IEnumerable<DeviceLog> query = await _dbSet.ToListAsync();
            return query;
        }

        public async Task<IEnumerable<DeviceLog>> GetByDate(DateTime date, Guid deviceId, bool deviceState)
        {
            var allLogs = await _dbSet.ToListAsync();
            IEnumerable<DeviceLog> result = allLogs.Where(log => log.DeviceId == deviceId && log.DateLogged.Date == date && log.DeviceState == deviceState);

            return result;
        }

        public async Task<DeviceLog?> GetByLatest(Guid deviceId)
        {
            var latestLog = await _dbSet
                .Where(l => l.DeviceId == deviceId)
                .OrderByDescending(l => l.DateLogged)
                .FirstOrDefaultAsync();

            return latestLog;
        }

        public async Task<IEnumerable<DeviceLog>> GetByRoom(Guid roomId) {
            var allLogs = await _dbSet.ToListAsync();
            IEnumerable<DeviceLog> result = allLogs.Where(allLogs => allLogs.RoomId == roomId);
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
