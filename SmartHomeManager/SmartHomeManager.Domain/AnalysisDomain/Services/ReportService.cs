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

        public async Task<PdfFile> GetDeviceReport(Guid deviceId)
        {
            string fileName = "device.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Get device
            Device? device = await _mockDeviceService.GetDeviceById(deviceId);

            // Get device log
            var deviceLog = await _deviceLogReadService.GetDeviceLogByIdAsync(deviceId);

            // TODO: Null check (validation) ...


            // Retrieve fileBytes using pdfBuilder
            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);
            pdfBuilder
                .addDeviceDetails(device)
                .addDeviceLogHeader();

            var totalUsage = 0.0;

            foreach(var log in deviceLog)
            {
                pdfBuilder.addDeviceLogById(log);
                totalUsage = totalUsage + log.DeviceEnergyUsage;
            }

           pdfBuilder.addDeviceLogTotalUsage(totalUsage)
                        .addGeneratedTime();
           
            var fileBytes = pdfBuilder.Build();


            return new PdfFile(fileBytes, "application/force-download", fileName);  
        }

        public async Task <PdfFile> GetHouseholdReport(Guid accountId)
        {
           
            string fileName = "household.pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(accountId);

            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);

            pdfBuilder
                .addHouseholdHeader(accountId);

            foreach (var device in deviceList)
            {
                pdfBuilder
                    .addHouseholdDetails(device);
            }

            pdfBuilder.addGeneratedTime();

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
