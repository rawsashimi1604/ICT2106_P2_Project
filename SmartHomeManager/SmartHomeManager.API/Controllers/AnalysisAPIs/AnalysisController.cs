using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace SmartHomeManager.API.Controllers.AnalysisAPIs
{
    [Route("api/analysis")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {
        // TODO: Add private service variables

        // TODO: Create constructor to inject services...

        // TODO: Create API Routes...

        // TODO: Device Route
        // GET /api/analysis/device/download/{deviceId}
        [HttpGet("device/download")]
        public ActionResult GetDeviceReport()
        {

            // string fileName = "2106_ClientMeeting.pdf";
            string fileName = "testing.pdf";

            // Create a new PDF document
            PdfDocument pdfDoc = new PdfDocument(new PdfWriter("../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName));
            Document doc = new Document(pdfDoc);

            // Add content to the PDF document
            Paragraph p1 = new Paragraph("Hello, World!").SetTextAlignment(TextAlignment.CENTER);
            doc.Add(p1);

            // Save the PDF document
            doc.Close();

            
            string filePath = "../SmartHomeManager.Domain/AnalysisDomain/Files/" + fileName;
            

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "application/force-download", fileName);
        }

        // TODO: HouseholdReport Route
        // GET /api/analysis/householdReport/download/{accountId}

        // TODO: HouseholdEnergyUsageForecast Route
        // GET /api/analysis/householdReport/energyUsageForecast/{accountId}

        // TODO: HouseholdEnergyEfficiency Route
        // GET /api/analysis/householdReport/energyEfficiency/{accountId}

        // TODO: CarbonFootprint Route
        // GET /api/analysis/householdReport/carbonFootprint/{accountId}
    }
}
