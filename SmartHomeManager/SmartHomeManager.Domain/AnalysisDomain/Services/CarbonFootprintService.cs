using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class CarbonFootprintService
    {

        private readonly IGenericRepository<CarbonFootprint> _carbonFootprintRepository;
        private readonly IDeviceInfoService _deviceLogService;

        public CarbonFootprintService(IGenericRepository<CarbonFootprint> carbonFootprintRepository, IDeviceLogRepository deviceLogRepository)
        {
            _carbonFootprintRepository = carbonFootprintRepository;
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
        }

        public async Task<string> GetCarbonFootprintAsync(Guid accountId, int month, int year)
        {
            // Check if the data exist in database

            // Get all the usage data belonging to one accountId
            // Find which device belong to which account...


            // Sum it all up
            // Compare it to the national
            // Add to the database
            // Return to controller

            // Test the device log service...
            var deviceLogs = await _deviceLogService.GetAllDeviceLogAsync();
            
            foreach (var deviceLog in deviceLogs)
            {
                System.Diagnostics.Debug.WriteLine("CarbonFootprintService: " + deviceLog.LogId + ":" + deviceLog.DeviceEnergyUsage);
            }

            return "carbon footprint";
        }

    }
}
