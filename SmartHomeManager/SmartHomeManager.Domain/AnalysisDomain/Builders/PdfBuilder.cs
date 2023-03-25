using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Entities;
using iText.Layout.Properties;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout.Borders;


namespace SmartHomeManager.Domain.AnalysisDomain.Builders
{
    public class PdfBuilder
    {
        private readonly Document _document;
        private readonly string _fileName;
        private const string FILEPATH = "../SmartHomeManager.Domain/AnalysisDomain/Files/";

        public PdfBuilder(string fileName, PdfDocument pdfDocument)
        {
            _document = new Document(pdfDocument);
            _fileName = fileName;
        }
//-----------------------------------------------------------------------------
        // Builder to add details for the device
        public PdfBuilder addDeviceDetails(Device device)
        {
            // Add header of the report
            _document.Add(new Paragraph($"Device {device.DeviceId} REPORT")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));

            // Header for Device table
            _document.Add(new Paragraph("Device Details")
                .SetBold()
                .SetUnderline()
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(15));

            // Create a table for device
            float[] deviceTableWidths = { 150F, 300F };
            Table deviceTable = new Table(deviceTableWidths);

            // Add cells to the device table
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device ID")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceId}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Name")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceName}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Brand")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceBrand}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Model")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceModel}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceType}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type Name")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceTypeName}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Serial Number")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceSerialNumber}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Watts")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceWatts}")));

            _document.Add(deviceTable);

            return this;
        }

//-----------------------------------------------------------------------------
        // Builder to add the device total energy usage
        public PdfBuilder addDeviceLogTotalUsage(double totalUsage)
        {
            _document.Add(new Paragraph($"Total Usage for Device : {totalUsage}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));
            return this;
        }


//-----------------------------------------------------------------------------
        // Builder to add the devices
        public PdfBuilder addHouseholdDetails(Device device) {

            // Create header for device table
            _document.Add(new Paragraph($"Device {device.DeviceId} Details")
                .SetBold()
                .SetFontSize(15)
                .SetUnderline());

            // Create a table for device
            float[] deviceTableWidths = { 150F, 300F };
            Table deviceTable = new Table(deviceTableWidths);


            // Add cells to the device table
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device ID")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceId}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Name")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceName}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Brand")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceBrand}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Model")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceModel}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceType}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Type Name")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceTypeName}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Serial Number")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceSerialNumber}")));
            deviceTable.AddCell(new Cell().Add(new Paragraph("Device Watts")
                .SetBold()));
            deviceTable.AddCell(new Cell().Add(new Paragraph($"{device.DeviceWatts}")));

            _document.Add(deviceTable).SetTextAlignment(TextAlignment.CENTER);

            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to add header for the household
        public PdfBuilder addHouseholdHeader(Guid accId)
        {
            _document.Add(new Paragraph($"Household Report For Account {accId}")
                .SetBold()
                .SetFontSize(20));
            return this;
        }
//-----------------------------------------------------------------------------
        //Builder to add total household energy usage
        public PdfBuilder addTotalHouseUsage(double householdUsage)
        {
            _document.Add(new Paragraph($"Total household energy usage is {householdUsage}")
                .SetTextAlignment(TextAlignment.CENTER).
                SetBold()
                .SetFontSize(15));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to generated the current time 
        public PdfBuilder addGeneratedTime()
        {
            // create date time 
            DateTime now = DateTime.Now;
            _document.Add(new Paragraph("Report Generated on: " + now)
                .SetTextAlignment(TextAlignment.RIGHT));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to test
        public PdfBuilder Date(DateTime start, DateTime end)
        {
            _document.Add(new Paragraph($"Report From {start} to {end}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(15));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to build the table for monthly stats

        public PdfBuilder addMonthlyStats(
            int lastMonths,
            List<String> allMonthYearStrings,
            List<double> allEnergyCost,
            List<double> allEnergyUsage
            )
        {

            _document.Add(new Paragraph("Monthly Stats")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold());

            // Create a table for device
            float[] tableWidths = { 150F, 150F, 150F };
            Table table = new Table(tableWidths);

            // Add table headers to the table
            table.AddCell(new Cell().Add(new Paragraph("Month")
                .SetBold()));
            table.AddCell(new Cell().Add(new Paragraph("Usage (W)")
                .SetBold()));
            table.AddCell(new Cell().Add(new Paragraph("Cost ($)")
                .SetBold()));


            // Add data to the table
            for(int i = 0; i < lastMonths; i++)
            {
                table.AddCell(new Cell().Add(new Paragraph(allMonthYearStrings[i])));
                table.AddCell(new Cell().Add(new Paragraph(allEnergyUsage[i].ToString("0.##"))));
                table.AddCell(new Cell().Add(new Paragraph(allEnergyCost[i].ToString("0.##"))));
            }

            _document.Add(table).SetTextAlignment(TextAlignment.CENTER);

            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to add the total usage and total cost
        public PdfBuilder addTotalUsageCost(double overallUsage, double overallCost)
        {
            _document.Add(new Paragraph($"Total Usage (W): {overallUsage.ToString("0.##")}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold());

            _document.Add(new Paragraph($"Total Cost ($): {overallCost.ToString("0.##")}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold());

            return this;
        }


//-----------------------------------------------------------------------------
        public PdfBuilder addCostChart(
            int lastMonths,
            List<String>allMonthYearStrings,
            List<double>allEnergyUsage
            )
        {
            String[] labels = { };
            double[] values = { };

            for(var i = 0; i < lastMonths; i++)
            {
                labels.Append(allMonthYearStrings[i]);
                values.Append(allEnergyUsage[i]);
            }

            // Define chart size and position
            float chartWidth = 400f;
            float chartHeight = 300f;
            float chartX = 100f;
            float chartY = 500f;

            // Define fonts for chart title and labels
            PdfFont titleFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont labelFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

            // Draw chart title
            Paragraph chartTitle = new Paragraph("Monthly Sales Report")
                .SetFont(titleFont)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(20f);
            _document.Add(chartTitle);

            // Draw chart labels
            Table labelTable = new Table(UnitValue.CreatePercentArray(new float[] { 30f, 70f }));
            for (int i = 0; i < labels.Length; i++)
            {
                Cell labelCell = new Cell().Add(new Paragraph(labels[i]))
                    .SetFont(labelFont)
                    .SetFontSize(10f)
                    .SetBorder(Border.NO_BORDER);
                labelTable.AddCell(labelCell);
                Cell valueCell = new Cell().Add(new Paragraph(values[i].ToString()))
                    .SetFont(labelFont)
                    .SetFontSize(10f)
                    .SetBorder(Border.NO_BORDER);
                labelTable.AddCell(valueCell);
            }
            _document.Add(labelTable);

            // Create chart
            



            return this;
        }

//-----------------------------------------------------------------------------
        public PdfBuilder addHouseholdOverall(double usage, double cost)
        {

            _document.Add(new Paragraph($"Total Household Usage (W): {usage.ToString("0.##")}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold());

            _document.Add(new Paragraph($"Total Household Cost ($): {cost.ToString("0.##")}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold());
            return this;
        }

//-----------------------------------------------------------------------------
        // Build the file
        public byte[] Build()
        {
            _document.Close();
            return System.IO.File.ReadAllBytes(FILEPATH + _fileName);
        }
    }
}
