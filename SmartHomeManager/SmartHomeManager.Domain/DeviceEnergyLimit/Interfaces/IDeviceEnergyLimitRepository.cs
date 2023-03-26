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
    Task<SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DeviceEnergyLimit> Get(Guid roomId);
    void Update(SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DeviceEnergyLimit entity);
    Task SaveChangesAsync();
    Task<IEnumerable<SmartHomeManager.Domain.DeviceEnergyLimit.Entities.DeviceEnergyLimit>> GetAll();
    //    public Task<DeviceEnergyLimit> GetDeviceEnergyLimitById(Guid id);
    //    public Task<IEnumerable<DeviceEnergyLimit>> GetAllDeviceEnergyLimit();
    //   public Task<bool> SetDeviceEnergyLimit(DeviceEnergyLimit deviceEnergyLimit);

}
