using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.DTOs;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using SmartHomeManager.Domain.NotificationDomain.Entities;
using SmartHomeManager.Domain.NotificationDomain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ForecastService
    {

        private readonly IForecastRepository _forecastRepository;
        private readonly IForecastDataRepository _forecastDataRepository;
        private readonly AccountService _accountService;
        private readonly IDeviceInfoService _deviceLogService;
        private readonly MockDeviceService _deviceService;

        public ForecastService(
            IForecastRepository forecastRepository,
            IForecastDataRepository forecastDataRepository,
            AccountService accountRepository,
            IDeviceRepository deviceRepository,
            IDeviceLogRepository deviceLogRepository
        )
        {
            _forecastRepository = forecastRepository;
            _forecastDataRepository= forecastDataRepository;
            _accountService = accountRepository;
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _deviceService = new MockDeviceService(deviceRepository);
        }

        public async Task<IEnumerable<ForecastChart>> GetHouseHoldForecast(Guid accountid,int timespan)
        {
            // Find which device belong to which account...
            //=> pass in account id and return all the device under that id
            IEnumerable<Device> devices = await _deviceService.GetAllDevicesByAccount(accountid);

            var accountToBeFound = await _accountService.GetAccountByAccountId(accountid);
            List<ForecastChart> results = new List<ForecastChart>();

            //Check if account exist
            if (accountToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Account not found");
                return results;
            }

            // Get the current date and time
            DateTime now = DateTime.Now;

            // Get the current month
            int month = now.Month;

            int count = 0;

            // Get the current year
            int year = now.Year;


            if (timespan == 0)
            {
                // Get the data for the past 6 months...
                List<DateTime> months = GetPastSixMonths(year, month);
                List<ForecastChart> result = new List<ForecastChart>();

                string guidStringChartDataId = "44444444-4444-4444-4444-444444444444";
                Guid guidChartDataId = Guid.Parse(guidStringChartDataId);

                string guidStringChartId = "33333333-3333-3333-3333-333333333333";
                Guid guidChartId = Guid.Parse(guidStringChartId);

                foreach (DateTime dt in months)
                {
                    // use rubin service to get all the logs by each device
                    IEnumerable<DeviceLog> deviceLogs = await _deviceLogService.GetAllDeviceLogAsync();
                    

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

                    int numDays = endDate.Subtract(startDate).Days;

                    // Calculate the average watts per day for each day in the month
                    for (int i = 1; i <= numDays; i++)
                    {
                        DateTime currentDate = new DateTime(dt.Year, dt.Month, i, 0, 0, 0);

                        double totalWatts = 0;
                        int numLogs = 0;
                        foreach (var deviceLog in deviceLogs)
                        {
                            if (deviceLog.DateLogged.Date == currentDate.Date)
                            {
                                totalWatts += deviceLog.DeviceEnergyUsage;
                                numLogs++;
                            }
                        }
                        double avgWattsPerDay = totalWatts / numLogs;
                        string date = string.Format("{0}/{1}/{2}", i, dt.Month, dt.Year);
                        // Print the average watts per day for the current day
                        Console.WriteLine("Average watts per day for {0}/{1}/{2}: {3}", i, dt.Month, dt.Year, avgWattsPerDay);

                        

                        ForecastChartData forecastChartData = new ForecastChartData
                        {
                            ForecastChartDataId = guidChartDataId,
                            ForecastChartId = guidChartId,
                            Label = date,
                            Value = avgWattsPerDay,
                            IsForecast = true,
                            Index = i,

                        };

                        try
                        {
                            await _forecastDataRepository.AddAsync(forecastChartData);
                        }
                        catch (Exception ex)
                        {
                            throw new DBInsertFailException();
                        }
                    }
                    ForecastChart forecastChart = new ForecastChart
                    {
                        ForecastChartId = guidChartId,
                        AccountId = accountid,
                        TimespanType = 1,
                        DateOfAnalysis = DateTime.Today.ToString("dd/MM"),
                    };

                    result.Add(forecastChart);

                }
            }

            // For average every month for current year and year before
            else if (timespan == 1)
            {
       
                List<ForecastChart> result = new List<ForecastChart>();

                // Define the start and end dates for the range of years you want to average
                DateTime startYear = DateTime.Now.AddYears(-1); // Current year - 1
                DateTime endYear = DateTime.Now; // Current year

                string guidStringChartDataId = "55555555-5555-5555-5555-555555555555";
                Guid guidChartDataId = Guid.Parse(guidStringChartDataId);

                string guidStringChartId = "44444444-4444-4444-4444-444444444444";
                Guid guidChartId = Guid.Parse(guidStringChartId);

                // Loop through each month
                for (int i = 1; i <= 12; i++)
                {
                    // Define the start and end dates for the month
                    DateTime startDate = new DateTime(startYear.Year, i, 1);
                    DateTime endDate = new DateTime(endYear.Year, i, 1).AddMonths(1).AddDays(-1); // Last day of the month


                    // Filter device logs by the specified date range
                    IEnumerable<DeviceLog> deviceLogs = await _deviceLogService.GetAllDeviceLogAsync();
                    
                    deviceLogs = deviceLogs.Where(deviceLog => deviceLog.DateLogged >= startDate && deviceLog.DateLogged <= endDate);

                    // Calculate the total watts for the month
                    double totalWatts = deviceLogs.Sum(deviceLog => deviceLog.DeviceEnergyUsage);

                    // Calculate the average watts for the month across the range of years
                    int yearCount = endYear.Year - startYear.Year + 1; // Number of years in the range
                    double averageWatts = totalWatts / yearCount;

                    // Output the average watts for the month
                    Console.WriteLine("Average watts for {0}: {1}", startDate.ToString("MMMM"), averageWatts);
                    
                    string date = string.Format("{0}/{1}", startDate.ToString("MMMM"), i);

                    

                    ForecastChartData forecastChartData = new ForecastChartData
                    {
                        ForecastChartDataId = guidChartDataId,
                        ForecastChartId = guidChartId,
                        Label = date,
                        Value = averageWatts,
                        IsForecast = true,
                        Index = i,

                    };

                    try
                    {
                        await _forecastDataRepository.AddAsync(forecastChartData);
                    }
                    catch (Exception ex)
                    {
                        throw new DBInsertFailException();
                    }

                }
                ForecastChart forecastChart = new ForecastChart
                {
                    ForecastChartId = guidChartId,
                    AccountId = accountid,
                    TimespanType = 0,
                    DateOfAnalysis = DateTime.Today.ToString("dd/MM"),
                };

                try
                {
                    await _forecastRepository.AddAsync(forecastChart);
                }
                catch (Exception ex)
                {
                    throw new DBInsertFailException();
                }

                result.Add(forecastChart);
            }
            

            return results;
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

        private List<DateTime> GetPastTwoYears(int year, int month)
        {
            List<DateTime> result = new List<DateTime>();

            DateTime now = new DateTime(year, month, 1);
            for (int i = 0; i < 24; i++)
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

        public async Task<IEnumerable<ForecastChartData>> GetHouseHoldForcastData(Guid forecastchardatatid)
        {
            var forecastChartToBeFound = await _forecastRepository.GetByIdAsync(forecastchardatatid);
            IEnumerable<ForecastChartData> allForecastChartData = null;

            //Check if account exist
            if (forecastChartToBeFound == null)
            {
                System.Diagnostics.Debug.WriteLine("Forecast Chart not found");
                return allForecastChartData;
            }
            allForecastChartData = await _forecastDataRepository.GetAllByIdAsync(forecastchardatatid);

            return allForecastChartData;
        }


    }
}
