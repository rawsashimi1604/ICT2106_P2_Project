using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.Exceptions
{
    [Serializable]
    public class DeviceNotFoundException : Exception
    {
        public DeviceNotFoundException() : base("Device not found") { }

    }
}
