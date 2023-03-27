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
using SmartHomeManager.Domain.RoomDomain.Interfaces;
using SmartHomeManager.Domain.RoomDomain.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Services
{
    // get all devices from profile first
    // get device log by date and time
    // get watts from devices

    public class DeviceLogReadService : IDeviceInfoService
    {
        private readonly IDeviceLogRepository _deviceLogRepository;

        public DeviceLogReadService(IDeviceLogRepository deviceLogRepository)
        {
            _deviceLogRepository = deviceLogRepository;
           
        }


        public IEnumerable<DeviceLog> GetDeviceLogByDay(Guid deviceId, DateTime date)
        {
            var logs = _deviceLogRepository.Get(deviceId, date);
            var deviceLogs = logs.Select(log => new DeviceLog
            {
                DeviceId = log.DeviceId,
                DeviceState = log.DeviceState,
                DeviceEnergyUsage = log.DeviceEnergyUsage,
                DeviceActivity = log.DeviceActivity
            }).ToList();

            return deviceLogs;
        }

        // look for logs (to update)
        public async Task<IEnumerable<DeviceLog>> GetDeviceLogByDate(DateTime date, Guid deviceId, bool deviceState)
        {
            var res = await _deviceLogRepository.GetByDate(date, deviceId, deviceState);
            
            return res;
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

        public async Task<DeviceLog?> GetLatestDeviceLog(Guid deviceId) {
            var res = await _deviceLogRepository.GetByLatest(deviceId);
            return res;
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

        public async Task<IEnumerable<DeviceLog>> GetDeviceLogByIdAsync(Guid deviceId)
        {
            var result = await _deviceLogRepository.GetAllByDeviceId(deviceId);
            return result;
        }
        
        // get logs based on roomId
        public async Task<IEnumerable<DeviceLog>> getDeviceLogByRoom(Guid roomId) {
            var res = await _deviceLogRepository.GetByRoom(roomId);
            return res;
        }
    }
}
