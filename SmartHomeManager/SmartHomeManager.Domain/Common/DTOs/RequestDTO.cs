using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.DTOs
{
    public abstract class RequestDTO
    {
        public IDTOData Request { get; set; }
    }
}
