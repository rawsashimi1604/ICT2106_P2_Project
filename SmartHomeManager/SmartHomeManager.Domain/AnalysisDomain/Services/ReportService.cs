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

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ReportService
    {
        private readonly MockDeviceService _mockDeviceService;
        private readonly DeviceLogReadService _deviceLogReadService;

        public ReportService(IDeviceRepository deviceRepository, IDeviceLogRepository deviceLogRepository)
        {
            _mockDeviceService = new(deviceRepository);
            _deviceLogReadService = new(deviceLogRepository);
        }

        public async Task<PdfFile> GetDeviceReport(Guid deviceId, string start, string end)
        {
            string fileName = "device.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Get device
            Device? device = await _mockDeviceService.GetDeviceById(deviceId);

            // Get device log
            IEnumerable<DeviceLog> deviceLog = await _deviceLogReadService.GetDeviceLogByIdAsync(deviceId);

            // Retrieve fileBytes using pdfBuilder
            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);
            pdfBuilder
                .addDeviceDetails(device);

            var totalUsage = 0.0;

            var parsedStart = DateTime.Parse(start);
            var parsedEnd = DateTime.Parse(end);

            pdfBuilder.Date(parsedStart, parsedEnd);

            foreach (var log in deviceLog)
            {
                if(log.DateLogged >= parsedStart && log.DateLogged <= parsedEnd)
                {
                    totalUsage = totalUsage + log.DeviceEnergyUsage;
                }
            }

           pdfBuilder.addDeviceLogTotalUsage(totalUsage)
                        .addGeneratedTime();
           
            var fileBytes = pdfBuilder.Build();


            return new PdfFile(fileBytes, "application/force-download", fileName);  
        }

        public async Task <PdfFile> GetHouseholdReport(Guid accountId, DateTime start, DateTime end)
        {
           
            string fileName = "household.pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(accountId);

            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);

            pdfBuilder
                .addHouseholdHeader(accountId);

            var totalHouseholdUsage = 0.0;

            foreach (var device in deviceList)
            {
                var totalDeviceUsage = 0.0;
                pdfBuilder
                    .addHouseholdDetails(device);
                // Get device log
                var deviceLog = await _deviceLogReadService.GetDeviceLogByIdAsync(device.DeviceId, start, end);
                foreach(var log in deviceLog)
                {
                    totalDeviceUsage = totalDeviceUsage + log.DeviceEnergyUsage;  
                }
                pdfBuilder.addDeviceLogTotalUsage(totalDeviceUsage);
                totalHouseholdUsage = totalHouseholdUsage + totalDeviceUsage;
            }
            pdfBuilder.addTotalHouseUsage(totalHouseholdUsage)
                      .addGeneratedTime();

            var filebytes = pdfBuilder.Build();

            return new PdfFile(filebytes, "application/force-download", fileName);
        }

        public async Task<IEnumerable<Device>?> GetDevicesByGUID(Guid accountId)
        {

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(accountId);

            return deviceList;


        }
    }
}
