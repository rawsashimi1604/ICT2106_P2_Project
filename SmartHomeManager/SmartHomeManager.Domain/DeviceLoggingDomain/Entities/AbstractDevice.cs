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
    public class AbstractDevices
    {
        private ICollection<IDeviceObserver> listeners;
        // constructor
        public AbstractDevice()
        {
            // start with an empty list of services
            listeners = new List<IDeviceObserver>();
        }

        // register a message service
        public void registerObserver(IDeviceObserver listener)
        {
            listeners.Add(listener);
        }
        // notify all message services
        public void notifyObservers(string message)
        {
            foreach (IDeviceObserver listener in listeners)
            {
                listener.Update(message);
            }
        }
    }


}
