using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using SmartHomeManager.Domain.AnalysisDomain.Builders;
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Interfaces;
using SmartHomeManager.Domain.DeviceDomain.Services;
using SmartHomeManager.Domain.DeviceLoggingDomain.Services;
using SmartHomeManager.Domain.DeviceLoggingDomain.Mocks;
using SmartHomeManager.Domain.DeviceLoggingDomain.Interfaces;
using System.Globalization;
using SmartHomeManager.Domain.AnalysisDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Interfaces;
using SmartHomeManager.Domain.AccountDomain.Services;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.Common.Exceptions;
using System.Web;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ReportService : IReport
    {
        private readonly IDeviceInformationService _deviceService;
        private readonly IDeviceLogInfoService _deviceLogService;
        private readonly IForecast _forecastService;
        private readonly IEnergyEfficiencyAnalytics _energyService;
        private readonly IAccountInfoService _accountService;

        private const double PRICE_PER_WATTS = 0.002;

        public ReportService(IDeviceRepository deviceRepository, IDeviceLogRepository deviceLogRepository, IForecast forecast, IEnergyEfficiencyAnalytics energy, IAccountRepository account)
        {
            _deviceService = new MockDeviceService(deviceRepository);
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _forecastService = forecast;
            _energyService = energy;
            _accountService = new AccountService(account);
        }

        public async Task<PdfFile> GetDeviceReport(Guid deviceId, int lastMonths)
        {
            Device? device = await _deviceService.GetDeviceByIdAsync(deviceId);

            string fileName = "device.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Get device log
            IEnumerable<DeviceLog> deviceLog = await _deviceLogService.GetDeviceLogByIdAsync(deviceId);

            // Retrieve fileBytes using pdfBuilder
            IPdfBuilder pdfBuilder = new PdfBuilder(fileName, pdfDoc);
            pdfBuilder
                .addDeviceDetails(device);


            var overallUsage = 0.0;
            var overallCost = 0.0;
            var pastLastMonths = GetPastLastMonths(DateTime.Now.Year, DateTime.Now.Month, lastMonths);

            List<String> allMonthYearStrings = new List<String>();
            List<double> allEnergyUsage = new List<double>();
            List<double> allEnergyCost = new List<double>();

            

     

            foreach (var monthDt in pastLastMonths)
            {
                System.Diagnostics.Debug.WriteLine("IN DEVICE PAST MONTHS : " + pastLastMonths);
                // If its december, end date should be Jan of the following year
                // If its not december, end date should be the following month 1st day.
                var endDate = monthDt.Month == 12 ?
                    new DateTime(monthDt.Year + 1, 1, 1, 0, 0, 0) :
                    new DateTime(monthDt.Year, monthDt.Month + 1, 1, 0, 0, 0);

                var monthData = await _deviceLogService.GetDeviceLogByIdAsync(deviceId,monthDt,endDate);

                var totalUsage = 0.0;

                foreach (var data in monthData)
                {
                    totalUsage += data.DeviceEnergyUsage;
                    overallUsage += data.DeviceEnergyUsage;
                }
                var totalEnergyCost = PRICE_PER_WATTS * totalUsage;
                overallCost += totalEnergyCost;
                var monthYearString = $"{monthDt.ToString("MMM")}-{monthDt.Year}";

                allMonthYearStrings.Add(monthYearString);
                allEnergyCost.Add(totalEnergyCost);
                allEnergyUsage.Add(totalUsage);
            }

            pdfBuilder.addMonthlyStats(lastMonths, allMonthYearStrings, allEnergyCost, allEnergyUsage)
                      .addTotalUsageCost(overallUsage, overallCost);

            EnergyEfficiency deviceEfficiency = await _energyService.GetDeviceEnergyEfficiency(deviceId);

            System.Diagnostics.Debug.WriteLine("HERE: " + deviceEfficiency.DeviceId);

            pdfBuilder
                .addDeviceEnergyHeader()
                .addDeviceEnergyEfficiency(deviceEfficiency)
                .addGeneratedTime();

           
            var fileBytes = pdfBuilder.Build();


            return new PdfFile(fileBytes, "application/force-download", fileName);  
        }

        public async Task <PdfFile> GetHouseholdReport(Guid accountId, int lastMonths)
        {
          
            Account? account = await _accountService.GetAccountByAccountId(accountId);

            string fileName = "household.pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            IEnumerable<Device> deviceList = await _deviceService.GetAllDevicesByAccountAsync(accountId);

            IPdfBuilder pdfBuilder = new PdfBuilder(fileName, pdfDoc);

            

            pdfBuilder
                .addHouseholdHeader(account)
                .addAccountDetails(account);

            var totalHouseholdUsage = 0.0;

            var pastLastMonths = GetPastLastMonths(DateTime.Now.Year, DateTime.Now.Month, lastMonths);
            var householdUsage = 0.0;
            var householdCost = 0.0;

            pdfBuilder.addDeviceParagraph();

            foreach (var device in deviceList)
            {
                var totalDeviceUsage = 0.0;
                pdfBuilder
                    .addHouseholdDetails(device);
                    

                List<String> allMonthYearStrings = new List<String>();
                List<double> allEnergyUsage = new List<double>();
                List<double> allEnergyCost = new List<double>();

                var overallUsage = 0.0;
                var overallCost = 0.0;
                
                foreach (var monthDt in pastLastMonths)
                {
                    // If its december, end date should be Jan of the following year
                    // If its not december, end date should be the following month 1st day.
                    var endDate = monthDt.Month == 12 ?
                        new DateTime(monthDt.Year + 1, 1, 1, 0, 0, 0) :
                        new DateTime(monthDt.Year, monthDt.Month + 1, 1, 0, 0, 0);

                    var monthData = await _deviceLogService.GetDeviceLogByIdAsync(device.DeviceId, monthDt, endDate);

                    var totalUsage = 0.0;

                    foreach (var data in monthData)
                    {
                        totalUsage += data.DeviceEnergyUsage;
                        overallUsage += data.DeviceEnergyUsage;
                    }
                    var totalEnergyCost = PRICE_PER_WATTS * totalUsage;
                    overallCost += totalEnergyCost;
                    var monthYearString = $"{monthDt.ToString("MMM")}-{monthDt.Year}";

                    allMonthYearStrings.Add(monthYearString);
                    allEnergyCost.Add(totalEnergyCost);
                    allEnergyUsage.Add(totalUsage);
                }

                householdUsage += overallUsage;
                householdCost += overallCost;

                pdfBuilder.addMonthlyStats(lastMonths, allMonthYearStrings, allEnergyCost, allEnergyUsage)
                      .addTotalUsageCost(overallUsage, overallCost);

            }
            pdfBuilder.addHouseholdOverall(householdUsage, householdCost);

            IEnumerable<ForecastChartData> forecastChartDatas = await _forecastService.GetHouseHoldForecast(accountId, 1);

            IEnumerable<EnergyEfficiency> energyEffiency = await _energyService.GetAllDeviceEnergyEfficiency(accountId);


            pdfBuilder.addForecastReport(forecastChartDatas)
                    .addEnergyHeader()
                    .addEnergyEfficiency(energyEffiency)
                    .addGeneratedTime();


            var filebytes = pdfBuilder.Build();

            return new PdfFile(filebytes, "application/force-download", fileName);
        }

        public async Task<IEnumerable<Device>?> GetDevicesByGUID(Guid accountId)
        {   
            IEnumerable<Device> deviceList = await _deviceService.GetAllDevicesByAccountAsync(accountId);
            return deviceList;
        }

        private List<DateTime> GetPastLastMonths(int year, int month, int lastMonths)
        {
            List<DateTime> result = new List<DateTime>();

            for (int i = 0; i < lastMonths; i++)
            {
                int dtYear = year;
                int dtMonth = month - i;
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
