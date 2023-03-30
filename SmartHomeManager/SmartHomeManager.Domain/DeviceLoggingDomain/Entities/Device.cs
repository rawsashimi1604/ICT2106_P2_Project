using SmartHomeManager.Domain.DeviceDomain.Entities;
using SmartHomeManager.Domain.RoomDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.DeviceLoggingDomain.Entities
{
    public class Device extends AbstractDevice
    {
    public turnOn()
    {
        // logic for turning device on
        this.notifyObservers(new DeviceState());
    }

    public turnOff()
    {
        // logic for turning device off
        this.notifyObservers(new DeviceState());
    }
}
