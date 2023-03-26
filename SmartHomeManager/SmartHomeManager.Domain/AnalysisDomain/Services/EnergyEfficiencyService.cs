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

        public EnergyEfficiencyService(IEnergyEfficiencyRepository energyEfficiencyRepository, IDeviceRepository deviceRepository, IAccountRepository accountRepository)
        {
            _energyEfficiencyRepository = energyEfficiencyRepository;
            _deviceService = new MockDeviceService(deviceRepository);
            _accountService = new AccountService(accountRepository);
        }

        public async Task<EnergyEfficiency> GetDeviceEnergyEfficiency(Guid deviceId)
        {
            try {
                EnergyEfficiency energyEfficiency = await _energyEfficiencyRepository.GetByDeviceIdAsync(deviceId);
                return energyEfficiency;
            }
            catch(Exception ex)
            {
                throw new DBReadFailException();
            }


        }
        public async Task<IEnumerable<EnergyEfficiency>> GetAllDeviceEnergyEfficiency(Guid accountId)
        {
            if (!await _accountService.CheckAccountExists(accountId))
            {
                throw new AccountNotFoundException();
            }

            List<EnergyEfficiency> result = new List<EnergyEfficiency>();
            IEnumerable<Device> allDevices = await _deviceService.GetAllDevicesByAccount(accountId);

            foreach (Device device in allDevices)
            {
                try
                {
                    EnergyEfficiency energyEfficiency = await GetDeviceEnergyEfficiency(device.DeviceId);
                    result.Add(energyEfficiency);
                }catch(Exception ex) { }
            }
            if (result.Count == 0)
            {
                throw new DBReadFailException();
            }

            return result;
        }

    }
}
