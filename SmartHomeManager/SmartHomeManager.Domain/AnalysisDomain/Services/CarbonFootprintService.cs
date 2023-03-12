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

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class CarbonFootprintService
    {

        private readonly IGenericRepository<CarbonFootprint> _carbonFootprintRepository;
        private readonly IDeviceInfoService _deviceLogService;
        private readonly IAccountService _accountService;

        public CarbonFootprintService(IGenericRepository<CarbonFootprint> carbonFootprintRepository, IDeviceLogRepository deviceLogRepository, IAccountRepository accountRepository)
        {
            _carbonFootprintRepository = carbonFootprintRepository;
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _accountService = new AccountService(accountRepository);
        }

        public async Task<string> GetCarbonFootprintAsync(Guid accountId, int month, int year)
        {

            // Check if the data exist in database
            //1. Check if account exists
            Account? account =  await _accountService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                throw new AccountNotFoundException();
            }
            //2. Check if month and year input are valid eg no -ve
            bool isMonthValid = month>=1 && month <= 12;
            bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
            if (!isMonthValid || !isYearValid)
            {
                throw new InvalidDateInputException();
            }
          
            //invalid throw exception - try catch 
            //3. if the data alr exist, eg jan 2023 exist, return the data from database

            //this is the flow where there is no data
            // Get all the usage data belonging to one accountId
            // 1. Find which device belong to which account...
            //=> pass in account id and return all the device under that id
            //2. use reubin service to get all the logs by each device

            // Sum it all up
            //1. Filter data to obtain data within month range eg 1-31st Jan same for year
            //2. sum everything up for month 
            //3. compare to the national household probs fix value
            //4. add it to database
            //5. return to controller
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
