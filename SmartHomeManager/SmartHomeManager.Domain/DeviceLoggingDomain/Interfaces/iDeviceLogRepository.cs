using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{

    public interface IDeviceLogRepository
    {
        // bfe log repo have to state the methods needed from ds
        IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date);
        IEnumerable<DeviceLog> Get(Guid deviceId, DateTime date, DateTime endTime);
        Task<IEnumerable<DeviceLog>> GetAsync(Guid deviceId, DateTime date, DateTime endTime);

        Task<IEnumerable<DeviceLog>> GetByDate(DateTime date, Guid deviceId, bool deviceState);

        Task<IEnumerable<DeviceLog>> GetAll();

        Task<IEnumerable<DeviceLog>> GetAllByDeviceId(Guid deviceId);

        //still missing write functions

        void Add(DeviceLog entity);

        Task<DeviceLog?> Get(DateTime date, Guid deviceId, bool deviceState);

        Task<DeviceLog?> Get(DateTime date);
        

        void Update(DeviceLog entity);
        Task<DeviceLog?> GetByLatest(Guid deviceId);

        Task<IEnumerable<DeviceLog>> GetByRoom(Guid roomId);

        Task SaveChangesAsync();
    }
}
