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
        // Builder to add the device log header
        public PdfBuilder addDeviceLogHeader()
        {
            // Create header for device log table
            _document.Add(new Paragraph("Device Log")
                .SetBold()
                .SetFontSize(15)
                .SetUnderline()
                .SetTextAlignment(TextAlignment.CENTER));
            return this;
        }
//-----------------------------------------------------------------------------
        // Builder to add the device total energy usage
        public PdfBuilder addDeviceLogTotalUsage(double totalUsage)
        {
            _document.Add(new Paragraph($"Total Usage for Device : {totalUsage}")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetBold()
                .SetFontSize(20));
            return this;
        }

//-----------------------------------------------------------------------------
        // Builder to add the device log by the device id
        public PdfBuilder addDeviceLogById(DeviceLog deviceLog)
        {
            _document.Add(new Paragraph($"Log {deviceLog.LogId}")
                .SetTextAlignment(TextAlignment.LEFT)
                .SetBold());

            // Create a table for device log
            float[] deviceLogTableWidth = { 150F, 300F };
            Table deviceLogTable = new Table(deviceLogTableWidth);

            // Add cells to the device log table
            deviceLogTable.AddCell(new Cell().Add(new Paragraph("Date Logged")
                .SetBold()));
            deviceLogTable.AddCell(new Cell().Add(new Paragraph($"{deviceLog.DateLogged}")));
            deviceLogTable.AddCell(new Cell().Add(new Paragraph("End Time")
                .SetBold()));
            deviceLogTable.AddCell(new Cell().Add(new Paragraph($"{deviceLog.EndTime}")));
            deviceLogTable.AddCell(new Cell().Add(new Paragraph("Energy Usage")
                .SetBold()));
            deviceLogTable.AddCell(new Cell().Add(new Paragraph($"{deviceLog.DeviceEnergyUsage}")));

            _document.Add(deviceLogTable);

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

            _document.Add(deviceTable);

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
        // Build the file
        public byte[] Build()
        {
            _document.Close();
            return System.IO.File.ReadAllBytes(FILEPATH + _fileName);
        }
    }
}
