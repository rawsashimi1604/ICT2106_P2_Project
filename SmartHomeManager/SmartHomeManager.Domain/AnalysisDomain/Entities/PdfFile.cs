using System;
namespace SmartHomeManager.Domain.AnalysisDomain.Entities
{
	public class PdfFile
	{
        public byte[] FileContents { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }

        public PdfFile(byte[] fileContents, string contentType, string fileName)
		{
			this.FileContents = fileContents;
			this.ContentType = contentType;
			this.FileName = fileName;
		}
	}
}

