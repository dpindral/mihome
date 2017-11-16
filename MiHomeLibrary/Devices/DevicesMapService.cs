using System;
using System.Collections.Generic;
using System.Linq;

namespace MiHomeLibrary.Devices
{
     
    public class DevicesMapService : IDevicesMap
    {
        private readonly List<DevicesGatewaySid> _map = new List<DevicesGatewaySid>();
        private static readonly object Locker = new object();
        
        public void SetGatewayDevices(string gatewaySid, string[] gatewayDevicesSids)
        {
            if (string.IsNullOrWhiteSpace(gatewaySid))
            {
                throw new ArgumentException(nameof(gatewaySid));
            }
            
            AddIfNotExists(gatewaySid, gatewaySid);
            
            if(gatewayDevicesSids == null) return;

            foreach (string devicesSid in gatewayDevicesSids)
            {
                AddIfNotExists(gatewaySid, devicesSid);
            }
            
        }

        private void AddIfNotExists(string gsid, string dsid)
        {
            lock (Locker)
            {
                if(_map.Any(x=>x.DeviceSid == dsid && x.GatewaySid==gsid)) return;
            
                _map.Add(new DevicesGatewaySid(){DeviceSid = dsid, GatewaySid = gsid});                
            }
            
        }

        public string GetGatewaySidForDevice(string deviceSid)
        {
            lock (Locker)
            {
                var maped = _map.FirstOrDefault(x => x.DeviceSid == deviceSid);
                return maped?.GatewaySid;    
            }
            
        }
    }
}