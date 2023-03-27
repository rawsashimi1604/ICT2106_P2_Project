using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common.Exceptions;

namespace SmartHomeManager.Domain.AnalysisDomain.Proxies
{
    public class EnergyEfficiencyProxy : IEnergyEfficiency
    {

        private readonly IAccountService _accountService;
        private readonly IEnergyEfficiency _energyEfficiencyService;

        public EnergyEfficiencyProxy(IEnergyEfficiency energyEfficiencyService, IAccountRepository accountRepository) 
        {
            _energyEfficiencyService= energyEfficiencyService;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<IEnumerable<EnergyEfficiency>> GetAllDeviceEnergyEfficiency(Guid accountId)
        {
            //Check if account exist
            if (!await _accountService.CheckAccountExists(accountId))
            {
                throw new AccountNotFoundException();
            }

            return await _energyEfficiencyService.GetAllDeviceEnergyEfficiency(accountId);
        }

        public async Task<EnergyEfficiency> GetDeviceEnergyEfficiency(Guid deviceId)
        {
            return await _energyEfficiencyService.GetDeviceEnergyEfficiency(deviceId);
        }
    }
}
