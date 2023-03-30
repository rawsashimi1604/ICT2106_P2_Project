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
    public class CarbonFootprintService : ICarbonFootprint
    {

        private readonly ICarbonFootprintRepository _carbonFootprintRepository;
        private readonly IDeviceLogInfoService _deviceLogService;
        private readonly IDeviceInformationService _deviceService;

        // According to the Energy Market Authority (EMA) of Singapore,
        // the average monthly electricity consumption per household in Singapore
        // is about 310 kilowatt-hours (kWh) as of 2021. 
        private const double NATIONAL_HOUSEHOLD_CONSUMPTION_MONTH_WATTS = 2700; // (for simulation)
        private const int HOURS_PER_MONTH = 730;

        public CarbonFootprintService(
            ICarbonFootprintRepository carbonFootprintRepository, 
            IDeviceLogRepository deviceLogRepository,
            IDeviceRepository deviceRepository
        )
        {
            _carbonFootprintRepository = carbonFootprintRepository;
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _deviceService = new MockDeviceService(deviceRepository);
        }

        public async Task<IEnumerable<CarbonFootprint>> GetCarbonFootprintAsync(Guid accountId, int month, int year)
        {

            //Data input such as accountexists and date validation has been implemented in proxy.

            //this is the flow where there is no data
            // Get all the usage data belonging to one accountId

            // Find which device belong to which account...
            //=> pass in account id and return all the device under that id
            IEnumerable<Device> devices = await _deviceService.GetAllDevicesByAccountAsync(accountId);
            
            

            // Get the data for the past 6 months...
            List<DateTime> months = GetPastSixMonths(year, month);
            List<CarbonFootprint> result = new List<CarbonFootprint>();

            foreach(DateTime dt in months)
            {
                System.Diagnostics.Debug.WriteLine(dt.Month + " " + dt.Year);
            }


            // For each month, add the data to database and create a DTO, add the resulting DTO to a list and return it to the controller.
            foreach (DateTime dt in months)
            {
                // use rubin service to get all the logs by each device
                IEnumerable<DeviceLog> deviceLogs = await _deviceLogService.GetAllDeviceLogAsync();


                // if the data alr exist, eg jan 2023 exist, return the data from database
                var carbonFootprintCheck = await _carbonFootprintRepository.GetCarbonFootprintByMonthAndYear(dt.Month, dt.Year);
                if (carbonFootprintCheck != null)
                {
                    result.Add(carbonFootprintCheck);
                    continue;
                }

                // Filter data to obtain data within month range eg 1-31st Jan same for year
                DateTime startDate = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
                DateTime endDate;

                // If its december, end date should be Jan of the following year
                // If its not december, end date should be the following month 1st day.
                endDate = dt.Month == 12 ?
                    new DateTime(dt.Year + 1, 1, 1, 0, 0, 0) :
                    new DateTime(dt.Year, dt.Month + 1, 1, 0, 0, 0);

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
                    System.Diagnostics.Debug.WriteLine("NotFound: " + dt.Month + " " + dt.Year);
                    throw new NoCarbonFootprintDataException();
                }

                // add it to database
                CarbonFootprint carbonFootprintData = new CarbonFootprint
                {
                    AccountId = accountId,
                    HouseholdConsumption = totalWatts / HOURS_PER_MONTH,
                    NationalHouseholdConsumption = NATIONAL_HOUSEHOLD_CONSUMPTION_MONTH_WATTS,
                    MonthOfAnalysis = dt.Month,
                    YearOfAnalysis = dt.Year
                };

                try
                {
                    await _carbonFootprintRepository.AddAsync(carbonFootprintData);
                }
                catch (Exception ex)
                {
                    throw new DBInsertFailException();
                }

                // Add it to a list
                result.Add(carbonFootprintData);
            }

            // 5. return to controller
            // Return to controller

            foreach (var cf in result)
            {
                System.Diagnostics.Debug.WriteLine(cf.MonthOfAnalysis + " " + cf.YearOfAnalysis);
            }

            return result;
        }


        private List<DateTime> GetPastSixMonths(int year, int month)
        {
            List<DateTime> result = new List<DateTime>();

            DateTime now = new DateTime(year, month, 1);
            for (int i = 0; i < 6; i++)
            {
                int dtYear = now.Year;
                int dtMonth = now.Month - i;
                if (dtMonth <= 0)
                {
                    dtMonth += 12;
                    dtYear -= 1;
                }

                DateTime dateTime = new DateTime(dtYear, dtMonth, 1);
                result.Add(dateTime);
            }

            return result;
        }
    }
}
