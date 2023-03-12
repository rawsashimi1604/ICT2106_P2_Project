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
    public class CarbonFootprintService
    {

        private readonly ICarbonFootprintRepository _carbonFootprintRepository;
        private readonly IDeviceInfoService _deviceLogService;
        private readonly IAccountService _accountService;
        private readonly MockDeviceService _deviceService;

        // According to the Energy Market Authority (EMA) of Singapore,
        // the average monthly electricity consumption per household in Singapore
        // is about 100 kilowatt-hours (kWh) as of 2021. 
        private const double NATIONAL_HOUSEHOLD_CONSUMPTION_MONTH_WATTS = 100000f; // 100000 Wh
        private const int HOURS_PER_MONTH = 730;

        public CarbonFootprintService(
            ICarbonFootprintRepository carbonFootprintRepository, 
            IDeviceLogRepository deviceLogRepository, 
            IAccountRepository accountRepository,
            IDeviceRepository deviceRepository
        )
        {
            _carbonFootprintRepository = carbonFootprintRepository;
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _accountService = new AccountService(accountRepository);
            _deviceService = new MockDeviceService(deviceRepository);
        }

        public async Task<CarbonFootprint> GetCarbonFootprintAsync(Guid accountId, int month, int year)
        {

            // Check if the data exist in database
            // 1. Check if account exists
            Account? account =  await _accountService.GetAccountByAccountId(accountId);
            if (account == null)
            {
                throw new AccountNotFoundException();
            }
            // 2. Check if month and year input are valid eg no -ve
            bool isMonthValid = month>=1 && month <= 12;
            bool isYearValid = year >= 2000 && year <= DateTime.Now.Year;
            if (!isMonthValid || !isYearValid)
            {
                throw new InvalidDateInputException();
            }

            // 3. if the data alr exist, eg jan 2023 exist, return the data from database
            var carbonFootprintCheck = await _carbonFootprintRepository.GetCarbonFootprintByMonthAndYear(month, year);
            if (carbonFootprintCheck != null)
            {
                return carbonFootprintCheck;
            }

            //this is the flow where there is no data
            // Get all the usage data belonging to one accountId

            // 1. Find which device belong to which account...
            //=> pass in account id and return all the device under that id
            IEnumerable<Device> devices = await _deviceService.GetAllDevicesByAccount(accountId);
            
            //2. use rubin service to get all the logs by each device
            IEnumerable<DeviceLog> deviceLogs = await _deviceLogService.GetAllDeviceLogAsync();

            // Filter data to obtain data within month range eg 1-31st Jan same for year
            DateTime startDate = new DateTime(year, month, 1, 0, 0, 0);
            DateTime endDate;

            // If its december, end date should be Jan of the following year
            // If its not december, end date should be the following month 1st day.
            endDate = month == 12 ? 
                new DateTime(year + 1, 1, 1, 0, 0, 0) : 
                new DateTime(year, month+1, 1, 0, 0, 0);

            // Filter device logs by the specified date...
            deviceLogs = deviceLogs.Where(deviceLog =>
                deviceLog.DateLogged >= startDate && deviceLog.DateLogged < endDate.AddDays(1)
            );

            // Sum of watts...
            // Sum it all up
            double totalWatts = 0;
            foreach (var deviceLog in deviceLogs)
            {
                totalWatts += deviceLog.DeviceEnergyUsage;
            }

            // If total watts has nothing, means there is no data logged.
            // If no data is logged, we throw an exception to controller
            if (totalWatts <= 0)
            {
                throw new NoCarbonFootprintDataException();
            }


            // 4. add it to database
            CarbonFootprint carbonFootprintData = new CarbonFootprint
            {
                AccountId = accountId,
                HouseholdConsumption = totalWatts / HOURS_PER_MONTH,
                NationalHouseholdConsumption = NATIONAL_HOUSEHOLD_CONSUMPTION_MONTH_WATTS,
                MonthOfAnalysis = month,
                YearOfAnalysis = year
            };

            try {
                await _carbonFootprintRepository.AddAsync(carbonFootprintData);
            }
            catch (Exception ex)
            {
                throw new DBInsertFailException();
            }

            // 5. return to controller
            // Return to controller
            return carbonFootprintData;
        }

    }
}
