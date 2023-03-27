using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;

namespace SmartHomeManager.Domain.DeviceDomain.Services
{
    public class RegisterDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceTypeRepository _deviceTypeRepository;

        public RegisterDeviceService(IDeviceRepository deviceRepository, IDeviceTypeRepository deviceTypeRepository)
        {
            _deviceRepository = deviceRepository;
            _deviceTypeRepository = deviceTypeRepository;
        }

        public async Task<IEnumerable<Device>> GetAllDevicesAsync()
        {
            return await _deviceRepository.GetAllAsync();
        }

        public async Task<IEnumerable<DeviceType>> GetAllDevicesTypeAsync()
        {
            return await _deviceTypeRepository.GetAllAsync();
        }

        public async Task<bool> RegisterDeviceAsync(string deviceName, string deviceBrand, string deviceModel, string deviceTypeName, int deviceWatts, string deviceSerialNumber, Guid accountId)
        {
            try
            {
                Device device = new()
                {
                    DeviceName = deviceName,
                    DeviceBrand = deviceBrand,
                    DeviceModel = deviceModel,
                    DeviceTypeName = deviceTypeName,
                    DeviceWatts = deviceWatts,
                    DeviceSerialNumber = deviceSerialNumber,
                    AccountId = accountId,
                };

                await _deviceRepository.AddAsync(device);

                return await _deviceRepository.SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> AddDeviceTypeAsync(DeviceType deviceType)
        {
            try
            {
                await _deviceTypeRepository.AddAsync(deviceType);

                return await _deviceTypeRepository.SaveAsync();
            }
            catch
            {
                return false;
            }
        }
    }
}
