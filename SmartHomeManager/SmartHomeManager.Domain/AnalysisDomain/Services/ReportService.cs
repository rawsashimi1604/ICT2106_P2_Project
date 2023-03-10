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

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ReportService
    {
        private readonly MockDeviceService _mockDeviceService;

        public ReportService(IDeviceRepository deviceRepository)
        {
            _mockDeviceService = new(deviceRepository);
        }
        

        public async Task<PdfFile> GetDeviceReport()
        {
            Guid tempDeviceId = Guid.Parse("33333333-3333-3333-3333-333333333333");
            string fileName = "device.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Get device
            Device? device = await _mockDeviceService.GetDeviceById(tempDeviceId);
            
            // Get device log

            // TODO: Null check (validation) ...


            // Retrieve fileBytes using pdfBuilder
            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);
            pdfBuilder
                .addDeviceDetails(device)
                .addGeneratedTime();
           
            var fileBytes = pdfBuilder.Build();


            return new PdfFile(fileBytes, "application/force-download", fileName);  
        }

        public async Task <PdfFile> GetHouseholdReport()
        {
            Guid tempAccId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            string fileName = "household.pdf";

            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(tempAccId);

            var pdfBuilder = new PdfBuilder(fileName, pdfDoc);

            pdfBuilder
                .addHouseholdHeader(tempAccId);

            foreach (var device in deviceList)
            {
                pdfBuilder
                    .addHouseholdDetails(device);
            }

            pdfBuilder.addGeneratedTime();

            var filebytes = pdfBuilder.Build();

            return new PdfFile(filebytes, "application/force-download", fileName);
        }

        public async Task<IEnumerable<Device>> GetDevicesByGUID()
        {
            Guid tempAccId = Guid.Parse("11111111-1111-1111-1111-111111111111");

            IEnumerable<Device> deviceList = await _mockDeviceService.GetAllDevicesByAccount(tempAccId);

            return deviceList;


        }
    }
}
