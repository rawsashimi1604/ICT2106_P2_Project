using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities.DTO;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.Domain.RoomDomain.Entities;
using static System.Reflection.Metadata.BlobBuilder;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Services
{
    // get all devices from profile first
    // get device log by date and time
    // get watts from devices

    public class DeviceLogReadService : IDeviceInfoService
    {
        private readonly IDeviceLogRepository _deviceLogRepository;
        private readonly IProfileService _profileService;
        //private readonly IDeviceWattsService _deviceWattsService;

        public DeviceLogReadService(IDeviceLogRepository deviceLogRepository)
        {
            _deviceLogRepository = deviceLogRepository;
           // _profileService = profileService;
           //_deviceWattsService = deviceWattsService;
        }

        // get devices from profile
       public IEnumerable<Device> GetAllDevicesInProfile(Guid profileId){
            return _profileService.GetDevicesByProfile(profileId);
        }


        // get watts from devices
/*        public int getDeviceWatts(Guid deviceId){
            return _deviceWattsService.getDeviceWatts(deviceId);
        }*/

        public IEnumerable<GetDeviceLogWebRequest> GetDeviceLogByDay(Guid deviceId, DateTime date)
        {
            var res = _deviceLogRepository.Get(deviceId, date);
            var resp = res.Select(log => new GetDeviceLogWebRequest
            {
                DeviceEnergyUsage = (int)log.DeviceEnergyUsage,
                DeviceActivity = (int)log.DeviceActivity,
            }).ToList();

            return resp;

        }

        // look for logs (to update)
        public async Task<GetDeviceLogWebRequest> GetDeviceLogByDate(DateTime date, Guid deviceId, bool deviceState)
        {
            var res = await _deviceLogRepository.GetByDate(date.Date, deviceId, deviceState);
            if (res == null) return null;

            var ret = new GetDeviceLogWebRequest
            {
                EndTime = res.EndTime,
                DateLogged = res.DateLogged,
                DeviceEnergyUsage = (int)res.DeviceEnergyUsage,
                DeviceActivity = (int)res.DeviceActivity,
                DeviceState = res.DeviceState
            };

            return ret;
            

        }
        // using this i already can get by week and day. 
        public IEnumerable<GetDeviceLogWebRequest> GetDeviceLogByDateAndTime(Guid deviceId, DateTime date, DateTime endTime)
        {
            var res =  _deviceLogRepository.Get(deviceId, date, endTime);
            var resp = res.Select(log => new GetDeviceLogWebRequest
            {
                DeviceEnergyUsage = (int)log.DeviceEnergyUsage,
                DeviceActivity = (int)log.DeviceActivity,
            }).ToList();

            return resp;

        }

        public async Task<IEnumerable<GetDeviceLogWebRequest>> GetAllDeviceLogs()
        {
            var result = await _deviceLogRepository.GetAll();
            var resp = result.Select(dLogs => new GetDeviceLogWebRequest
            {
                EndTime = dLogs.EndTime,
                DateLogged = dLogs.DateLogged,
                DeviceEnergyUsage = (int)dLogs.DeviceEnergyUsage,
                DeviceActivity = (int)dLogs.DeviceActivity,
                DeviceState = dLogs.DeviceState

            }).ToList();
            return resp;
        }

        // Mocks for team5...
        public async Task<IEnumerable<DeviceLog>> GetAllDeviceLogAsync()
        {
            var result = await _deviceLogRepository.GetAll();
            return result;
        }

        public async Task<IEnumerable<DeviceLog>> GetDeviceLogByIdAsync(Guid deviceId, DateTime start, DateTime end)
        {
            var result = await _deviceLogRepository.GetAsync(deviceId, start, end);
            return result;
        }
    }
}
