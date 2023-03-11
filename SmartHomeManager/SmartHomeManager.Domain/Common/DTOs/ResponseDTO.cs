using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.DTOs
{
    public abstract class ResponseDTO
    {
        public IEnumerable<IDTOData> Data { get; set; }  
        public ResponseInformation Response { get; set; }
    }
}
