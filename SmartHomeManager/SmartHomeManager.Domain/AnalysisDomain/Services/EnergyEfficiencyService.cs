using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class EnergyEfficiencyService : IEnergyEfficiency
    {
        private readonly IEnergyEfficiencyRepository _energyEfficiencyRepository;
        private readonly MockDeviceService _deviceService;
        private readonly AccountService _accountService;
        private readonly IDeviceInfoService _deviceInfoService;

        //Within how manay days is counted as recent data
        private static int RECENTDAYS = 7;
        //Total energy usage for how many days 
        private static int NUMOFDAYS = 30;

        public EnergyEfficiencyService(IEnergyEfficiencyRepository energyEfficiencyRepository, IDeviceRepository deviceRepository, IAccountRepository accountRepository, IDeviceLogRepository deviceLogRepository)
        {
            _energyEfficiencyRepository = energyEfficiencyRepository;
            _deviceService = new MockDeviceService(deviceRepository);
            _accountService = new AccountService(accountRepository);
            _deviceInfoService = new DeviceLogReadService(deviceLogRepository);
        }


        public async Task<EnergyEfficiency> GetDeviceEnergyEfficiency(Guid deviceId)
        {
            try
            {
                EnergyEfficiency energyEfficiency = await _energyEfficiencyRepository.GetByDeviceIdAsync(deviceId);
                if (energyEfficiency == null)
                {
                    Device device = await _deviceService.GetDeviceById(deviceId);
                    //Calculate total Usage
                    double totalUsage = 0;
                    IEnumerable<DeviceLog> deviceLogs = await _deviceInfoService.GetDeviceLogByIdAsync(device.DeviceId, DateTime.Now.AddDays(-NUMOFDAYS).Date, DateTime.Now);
                    foreach (DeviceLog deviceLog in deviceLogs)
                    {
                        totalUsage += deviceLog.DeviceEnergyUsage;
                    }
                    double averageTotalUsage = totalUsage / NUMOFDAYS;
                    double nationalAverage = getAverageWatt("Else");
                    DeviceType deviceType = device.DeviceType;
                    if (deviceType != null)
                    {
                        nationalAverage = getAverageWatt(deviceType.DeviceTypeName);
                    }
                    //Calculate EEI
                    double EEI = Math.Min(nationalAverage / averageTotalUsage * 100, 100);

                    //Create new EnergyEfficiency Object and add it into the database and list
                    energyEfficiency = new EnergyEfficiency
                    {
                        EnergyEfficiencyId = Guid.NewGuid(),
                        DeviceId = device.DeviceId,
                        EnergyEfficiencyIndex = EEI,
                        Device = device,
                        DateOfAnalysis = DateTime.Now
                    };
                    _energyEfficiencyRepository.AddAsync(energyEfficiency);
                }
                return energyEfficiency;
            }
            catch (Exception ex)
            {
                throw new DBReadFailException();
            }


        }
        public async Task<IEnumerable<EnergyEfficiency>> GetAllDeviceEnergyEfficiency(Guid accountId)
        {
            //Add all DeviceEnergyUsage


            //Check if account exist
            if (!await _accountService.CheckAccountExists(accountId))
            {
                throw new AccountNotFoundException();
            }


            //get All device
            IEnumerable<Device> allDevices = await _deviceService.GetAllDevicesByAccount(accountId);
            List<EnergyEfficiency> allEnergyEfficiency = new List<EnergyEfficiency>();

            //get pass 30 days Compare to EditTime in DeviceLogs Table
            foreach (Device device in allDevices)
            {
                //Get from Energy Efficiency Table
                EnergyEfficiency existingEf = await _energyEfficiencyRepository.GetByDeviceIdAsync(device.DeviceId);
                Boolean toUpdate = true;
                //If exist, check if it is out of date
                if (existingEf != null)
                {
                    //Outdated
                    if (existingEf.DateOfAnalysis.AddDays(RECENTDAYS) < DateTime.Now)
                    {
                        _energyEfficiencyRepository.DeleteByIdAsync(existingEf.EnergyEfficiencyId);
                    }
                    //Recent
                    else
                    {
                        allEnergyEfficiency.Add(existingEf);
                        toUpdate = false;
                    }
                }
                if (toUpdate)
                {
                    allEnergyEfficiency.Add(await GetDeviceEnergyEfficiency(device.DeviceId));
                }
            }
            return allEnergyEfficiency;
        }

        private double getAverageWatt(String deviceName)
        {
            switch (deviceName)
            {
                case "Fan":
                    return 75;
                case "Light":
                    return 40;
                case "Aircon":
                    return 2100;
                default:
                    return 500.0;
            }
            return 0.0;
        }

    }
}
