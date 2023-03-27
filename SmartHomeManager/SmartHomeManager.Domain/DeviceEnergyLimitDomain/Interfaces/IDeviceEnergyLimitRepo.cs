using SmartHomeManager.Domain.DeviceEnergyLimit.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceEnergyLimit.Interfaces;

public interface IDeviceEnergyLimitRepository
{
	Task<DeviceEnergyLimit> Get(Guid roomId);
	void Update(SmartHomeManager.Domain.DeviceEnergyLimitDomain.Entities.DeviceEnergyLimit  entity);
	Task SaveChangesAsync();
	Task<IEnumerable<DeviceEnergyLimit>> GetAll();
	Task<DeviceLog> Get(Guid deviceId, DateTime dateTime);
	Task<IEnumerable<DeviceLog>> Get(Guid deviceId, DateTime startDateTime, DateTime endDateTime);
	Task<IEnumerable<DeviceLog>> GetByDate(DateTime dateTime, Guid deviceId, bool deviceState);
	Task<IEnumerable<DeviceLog>> Get(DateTime dateTime, Guid deviceId, bool deviceState);
	Task<IEnumerable<DeviceLog>> Get(DateTime dateTime);
	Task Add(DeviceLog entity);
}
