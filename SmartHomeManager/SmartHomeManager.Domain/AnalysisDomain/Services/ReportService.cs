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
using SmartHomeManager.Domain.AnalysisDomain.Entities;
using SmartHomeManager.Domain.DeviceDomain.Entities;
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
            string fileName = "testing.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Get all devices
            Device? device = await _mockDeviceService.GetDeviceById(tempDeviceId);
            // TODO: Null check (validation) ...


            // Add content to the PDF document
            Paragraph p1 = new Paragraph("Hello, World!").SetTextAlignment(TextAlignment.CENTER);
            doc.Add(p1);

            // Add device id and name to pdf document
            Paragraph paragraph = new Paragraph($"{device.DeviceId} {device.DeviceName}")
                .SetTextAlignment(TextAlignment.CENTER);
            doc.Add(paragraph);


            // Save the PDF document
            doc.Close();

            string filePath = "../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName;
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return new PdfFile(fileBytes, "application/force-download", fileName);
        }

        public void GetHouseholdReport()
        {
            throw new NotImplementedException();
        }
    }
}
