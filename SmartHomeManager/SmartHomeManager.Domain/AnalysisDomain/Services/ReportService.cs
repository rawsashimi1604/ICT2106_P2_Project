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
using System.Web;

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ReportService
    {
        private readonly MockDeviceService _mockDeviceService;
        private readonly IDeviceInfoService _deviceLogService;
        private readonly ICarbonFootprint _carbonFootprintService;
        private readonly IForecast _forecastService;
        private const double PRICE_PER_WATTS = 0.002;

        public ReportService(IDeviceRepository deviceRepository, IDeviceLogRepository deviceLogRepository, IForecast forecast)
        {
            _mockDeviceService = new(deviceRepository);
            _deviceLogService = new DeviceLogReadService(deviceLogRepository);
            _forecastService = forecast;
        }

        public async Task<PdfFile> GetDeviceReport(Guid deviceId, int lastMonths)
        {
            string fileName = "device.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Get device
            Device? device = await _mockDeviceService.GetDeviceById(deviceId);

            // Get device log
            IEnumerable<DeviceLog> deviceLog = await _deviceLogService.GetDeviceLogByIdAsync(deviceId);

            // Retrieve fileBytes using pdfBuilder
            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);
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
                      .addTotalUsageCost(overallUsage,overallCost)
                      .addGeneratedTime();

           
            var fileBytes = pdfBuilder.Build();


            return new PdfFile(fileBytes, "application/force-download", fileName);  
        }

        public async Task <PdfFile> GetHouseholdReport(Guid accountId, int lastMonths)
        {
           
            string fileName = "household.pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(accountId);

            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);

            pdfBuilder
                .addHouseholdHeader(accountId);

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

            pdfBuilder.addForecastReport(forecastChartDatas)
                 .addGeneratedTime();


            var filebytes = pdfBuilder.Build();

            return new PdfFile(filebytes, "application/force-download", fileName);
        }

        public async Task<IEnumerable<Device>?> GetDevicesByGUID(Guid accountId)
        {

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(accountId);

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

        private void GetBarChart(int lastMonths, List<String>allMonthYearString, List<double>values)
        {
            // Create a new instance of the Chart class

            // Set the chart properties
            //myChart.Width = new Unit(400);


            for(int i = 0; i < lastMonths; i++)
            {
                
            }
        }
    }
}
