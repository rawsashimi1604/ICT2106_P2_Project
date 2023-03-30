using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.Common.Exceptions;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;


namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ForecastService : IForecast
    {

        private readonly IForecastDataRepository _forecastDataRepository;
        private readonly IDeviceLogInfoService _deviceLogService;
        private readonly IDeviceInformationService _deviceService;
        private const double PRICE_PER_WATTS = 0.002;

        public ForecastService(
            IForecastDataRepository forecastDataRepository,
            IAccountRepository accountRepository,
            IDeviceRepository deviceRepository,
            IDeviceLogRepository deviceLogRepository
        )
        {
            _forecastDataRepository= forecastDataRepository;
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _deviceService = new MockDeviceService(deviceRepository);
        }

        public async Task<IEnumerable<ForecastChartData>> GetHouseHoldForecast(Guid accountId, int timespan)
        {
            // Find which device belong to which account...
            //=> pass in account id and return all the device under that id
            IEnumerable<Device> devices = await _deviceService.GetAllDevicesByAccountAsync(accountId);

            List<ForecastChartData> result = new List<ForecastChartData>();
            // Get the current date and time
            DateTime now = DateTime.Now;

            // Get the current month
            int month = now.Month;

            // Get the current year
            int year = now.Year;

            double priceValue = 0;

            // Check if the current date / timespan already has a forecast done...
            string dateOfAnalysis = DateTime.Today.ToString("dd/MM");
            IEnumerable<ForecastChartData> cachedData = await _forecastDataRepository.GetCachedData(accountId, dateOfAnalysis, timespan);

            // Data already exists, no need to recalculate...
            if (cachedData.Count() > 0)
            {
                return cachedData;
            }


            if (timespan == 0)
            {
                // Get the data for the past 6 months...
                List<DateTime> months = GetPastSixMonths(year, month);
                double totalWatts = 0;
                int numLogs = 0;
                DateTime today = DateTime.Today;
                DateTime nextMonth = today.AddMonths(1);
                string nextMonthString = nextMonth.ToString("MM");
                int thisYear = DateTime.Now.Year;
                int daysInMonth = DateTime.DaysInMonth(thisYear, DateTime.Now.Month);
                double avgWattsPerDay = 0;

                for (int i = 1; i <= daysInMonth; i++)
                {
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

                        DateTime currentDate = new DateTime(dt.Year, dt.Month, i, 0, 0, 0);

                        
                        foreach (var deviceLog in deviceLogs)
                        {
                            if (deviceLog.DateLogged.Date == currentDate.Date)
                            {
                                totalWatts += deviceLog.DeviceEnergyUsage;
                                numLogs++;
                            }
                        }
                        avgWattsPerDay = totalWatts/ numLogs;
                        priceValue = avgWattsPerDay * PRICE_PER_WATTS;

                    }

                    string date = string.Format("{0}/{1}", i, (month+1));

                    // Insert ForecastChartData into the database...
                    ForecastChartData forecastChartData = new ForecastChartData
                    {
                        ForecastChartDataId = Guid.NewGuid(),
                        AccountId = accountId,
                        TimespanType = timespan,
                        DateOfAnalysis = DateTime.Today.ToString("dd/MM"),
                        Label = date,
                        WattsValue = avgWattsPerDay,
                        PriceValue = priceValue,
                        Index = i,
                    };

                    result.Add(forecastChartData);

                    try
                    {
                        await _forecastDataRepository.AddAsync(forecastChartData);
                    }
                    catch (Exception ex)
                    {
                        throw new DBInsertFailException();
                    }
                }
            }

            // For average every month for current year and year before
            else if (timespan == 1)
            {

                // Define the start and end dates for the range of years you want to average
                DateTime startYear = DateTime.Now.AddYears(-1); // Current year - 1
                DateTime endYear = DateTime.Now; // Current year

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

                    
                    double averageWatts = totalWatts / 96;
                    priceValue= averageWatts * PRICE_PER_WATTS;

                    // Output the average watts for the month
                    Console.WriteLine("Average watts for {0}: {1}", startDate.ToString("MMMM"), averageWatts);
                    
                    string date = string.Format("{0}", startDate.ToString("MMMM"));


                    ForecastChartData forecastChartData = new ForecastChartData
                    {
                        ForecastChartDataId = Guid.NewGuid(),
                        AccountId = accountId,
                        TimespanType = timespan,
                        DateOfAnalysis = DateTime.Today.ToString("dd/MM"),
                        Label = date,
                        WattsValue = averageWatts,
                        PriceValue = priceValue,
                        Index = i,
                    };

                    result.Add(forecastChartData);

                    try
                    {
                        await _forecastDataRepository.AddAsync(forecastChartData);
                    }
                    catch (Exception ex)
                    {
                        throw new DBInsertFailException();
                    }

                }


                
            }

            if (timespan == 2)
            {
                // Get the data for the past 6 months...
                List<DateTime> months = GetPastSixMonths(year, month);
                double totalWatts = 0;
                int numLogs = 0;
                DateTime today = DateTime.Today;
                DateTime nextMonth = today.AddMonths(1);
                string nextMonthString = nextMonth.ToString("MMMM");
                int thisYear = DateTime.Now.Year;
                int daysInMonth = DateTime.DaysInMonth(thisYear, DateTime.Now.Month);
                double avgWattsPerDay = 0;
                string day;

                for (int i = 1; i <= 7; i++)
                {
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

                        DateTime currentDate = new DateTime(dt.Year, dt.Month, i, 0, 0, 0);


                        foreach (var deviceLog in deviceLogs)
                        {
                            if (deviceLog.DateLogged.Date == currentDate.Date)
                            {
                                totalWatts += deviceLog.DeviceEnergyUsage;
                                numLogs++;
                            }
                        }
                        avgWattsPerDay = totalWatts / numLogs;
                        priceValue = avgWattsPerDay * 0.002;

                    }

                    switch (i)
                    {
                        case 1:
                            day = "Monday";
                            break;
                        case 2:
                            day = "Tuesday";
                            break;
                        case 3:
                            day = "Wednesday";
                            break;
                        case 4:
                            day = "Thursday";
                            break;
                        case 5:
                            day = "Friday";
                            break;
                        case 6:
                            day = "Saturday";
                            break;
                        case 7:
                            day = "Sunday";
                            break;
                        default:
                            day = "Invalid day number";
                            break;
                    }



                    // Insert ForecastChartData into the database...
                    ForecastChartData forecastChartData = new ForecastChartData
                    {
                        ForecastChartDataId = Guid.NewGuid(),
                        AccountId = accountId,
                        TimespanType = timespan,
                        DateOfAnalysis = DateTime.Today.ToString("dd/MM"),
                        Label = day,
                        WattsValue = avgWattsPerDay,
                        PriceValue = priceValue,
                        Index = i,
                    };

                    result.Add(forecastChartData);

                    try
                    {
                        await _forecastDataRepository.AddAsync(forecastChartData);
                    }
                    catch (Exception ex)
                    {
                        throw new DBInsertFailException();
                    }
                }
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
    }
}
