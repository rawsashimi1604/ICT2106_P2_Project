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
using SmartHomeManager.Domain.NotificationDomain.Services;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class CarbonFootprintProxy : ICarbonFootprint
    {

        private readonly IAccountInfoService _accountService;
        private readonly ICarbonFootprint _carbonFootprintService;

        public CarbonFootprintProxy(ICarbonFootprint carbonFootprintService,IAccountRepository accountRepository)
        {
            _carbonFootprintService = carbonFootprintService;
            _accountService = new AccountService(accountRepository);
        }

        public async Task<IEnumerable<CarbonFootprint>> GetCarbonFootprintAsync(Guid accountId, int month, int year)
        {

            // Check if the data exist in database
            //1. Check if account exists
            Account? account = await _accountService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                throw new AccountNotFoundException();
            }
            //2. Check if month and year input are valid eg no -ve
            bool isMonthValid = month >= 1 && month <= 12;
            bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
            if (!isMonthValid || !isYearValid)
            {
                throw new InvalidDateInputException();
            }

            return await _carbonFootprintService.GetCarbonFootprintAsync(accountId, month, year);
        }

    }
}

