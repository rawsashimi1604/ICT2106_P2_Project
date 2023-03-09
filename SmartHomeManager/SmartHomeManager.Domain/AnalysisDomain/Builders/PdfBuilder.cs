using System;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.DeviceLoggingDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Entities;
using iText.Layout.Properties;

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
                .SetTextAlignment(TextAlignment.LEFT));

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

            _document.Add(deviceTable);

            return this;
        }

        public PdfBuilder addHouseholdHeader(Guid accId)
        {
            _document.Add(new Paragraph($"Household Report For Account {accId}")
                .SetBold()
                .SetFontSize(20));
            return this;
        }


        public PdfBuilder addGeneratedTime()
        {
            // create date time 
            DateTime now = DateTime.Now;
            _document.Add(new Paragraph("Report Generated on: " + now)
                .SetTextAlignment(TextAlignment.RIGHT));
            return this;
        }

        public byte[] Build()
        {
            _document.Close();
            return System.IO.File.ReadAllBytes(FILEPATH + _fileName);
        }
    }
}

