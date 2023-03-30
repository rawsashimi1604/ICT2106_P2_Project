using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces
{
    // To be passed into Team5 - Analysis.
    public interface IDeviceLogInfoService
    {
        public Task<IEnumerable<DeviceLog>> GetAllDeviceLogAsync();

        public Task<IEnumerable<DeviceLog>> GetDeviceLogByIdAsync(Guid deviceId, DateTime start, DateTime end);

        public Task<IEnumerable<DeviceLog>> GetDeviceLogByIdAsync(Guid deviceId);

    }
}
