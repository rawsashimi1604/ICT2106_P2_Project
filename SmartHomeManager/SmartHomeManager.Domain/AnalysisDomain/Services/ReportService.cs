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

namespace SmartHomeManager.Domain.AnalysisDomain.Services
{
    public class ReportService
    {
        public PdfFile GetDeviceReport()
        {
            string fileName = "testing.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            iText.Layout.Document doc = new iText.Layout.Document(pdfDoc);

            // Add content to the PDF document
            Paragraph p1 = new Paragraph("Hello, World!").SetTextAlignment(TextAlignment.CENTER);
            doc.Add(p1);

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
