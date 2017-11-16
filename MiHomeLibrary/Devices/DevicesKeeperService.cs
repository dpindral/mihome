using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MiHomeLibrary.Communication;
using MiHomeLibrary.Communication.Responses;

namespace MiHomeLibrary.Devices
{
    public class DevicesKeeperService : IDevicesKeeper
    {
        private readonly List<BaseDevice> _devices;

        public DevicesKeeperService()
        {
            _devices = new List<BaseDevice>();
        }

        public IEnumerable<BaseDevice> GetDevices()
        {
            return _devices;
        }

        public async Task UpdateDevicesList(DiscoverDeviceRsp response, IMiCommunication communication)
        {
            var device = _devices.FirstOrDefault(x => x.Sid == response.sid);
            if (device != null)
            {
                device.UpdateDevice(response.data);
                return;
            }

            if (response.model == "gateway")
            {
                Gateway g = new Gateway(response, null, communication);
                g.UpdateDevice(response.data);
                _devices.Add(g);
                return;
            }

            if (response.model == "sensor_ht")
            {
                TemperatureSensor g = new TemperatureSensor(response, null, communication);
                g.UpdateDevice(response.data);
                _devices.Add(g);
                return;
            }
                

        }
    }
}