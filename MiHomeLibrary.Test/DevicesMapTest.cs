using System;
using MiHomeLibrary.Devices;
using NUnit.Framework;

namespace MiHomeLibrary.Test
{
    [TestFixture]
    public class DevicesMapTest
    {
        [Test]
        public void SetGatewaySidForDevice_Normal_OK()
        {
            string gatewaySid = "aaa";
            string sid1 = "bbb";
            string sid2 = "ccc";
            string[] devices = new string[]{sid1,sid2};
            DevicesMapService devicesMap = new DevicesMapService();
            devicesMap.SetGatewayDevices(gatewaySid,devices);
            
            Assert.That(devicesMap.GetGatewaySidForDevice(sid1),Is.EqualTo(gatewaySid));
            Assert.That(devicesMap.GetGatewaySidForDevice(sid2),Is.EqualTo(gatewaySid));
            
        }

        [Test]
        public void SetGatewaySidForDevice_GatewaySid_OK()
        {
            string gatewaySid = "aaa";
            string sid1 = "bbb";
            string sid2 = "ccc";
            string[] devices = new string[] {sid1, sid2};
            DevicesMapService devicesMap = new DevicesMapService();
            devicesMap.SetGatewayDevices(gatewaySid, devices);

            Assert.That(devicesMap.GetGatewaySidForDevice(gatewaySid), Is.EqualTo(gatewaySid));
        }
        
        [Test]
        public void SetGatewaySidForDevice_ManyGateways_OK()
        {
            string gatewaySid1 = "aaa";
            string gatewaySid2 = "ddd";
            string sid1 = "bbb";
            string sid2 = "ccc";
            
            string[] devices1 = new string[] {sid1};
            string[] devices2 = new string[] {sid2};
            DevicesMapService devicesMap = new DevicesMapService();
            devicesMap.SetGatewayDevices(gatewaySid1, devices1);
            devicesMap.SetGatewayDevices(gatewaySid2, devices2);

            Assert.That(devicesMap.GetGatewaySidForDevice(sid1), Is.EqualTo(gatewaySid1));
            Assert.That(devicesMap.GetGatewaySidForDevice(sid2), Is.EqualTo(gatewaySid2));
            
        }

        [Test]
        public void SetGatewaySidForDevice_NoGatewaySid_ThrowsError()
        {
            DevicesMapService devicesMap = new DevicesMapService();
            Assert.Throws<ArgumentException>(() => { devicesMap.SetGatewayDevices("", null); });
        }
        
        [Test]
        public void SetGatewaySidForDevice_NullGatewaySid_ThrowsError()
        {
            DevicesMapService devicesMap = new DevicesMapService();
            Assert.Throws<ArgumentException>(() => { devicesMap.SetGatewayDevices(null, null); });
        }
        
        [Test]
        public void SetGatewaySidForDevice_NullAsDevicesList_NoError()
        {
            string gatewaySid1 = "aaa";
            DevicesMapService devicesMap = new DevicesMapService();
            devicesMap.SetGatewayDevices(gatewaySid1, null);
            
            Assert.That(devicesMap.GetGatewaySidForDevice(gatewaySid1), Is.EqualTo(gatewaySid1));
        }
        
        [Test]
        public void SetGatewaySidForDevice_EmptyDevicesList_NoError()
        {
            string gatewaySid1 = "aaa";
            string[] devices1 = new string[0];
                
            DevicesMapService devicesMap = new DevicesMapService();
            devicesMap.SetGatewayDevices(gatewaySid1, devices1);
            
            Assert.That(devicesMap.GetGatewaySidForDevice(gatewaySid1), Is.EqualTo(gatewaySid1));
        }
        
        [Test]
        public void GetGatewaySidForDevice_DeviceOutOfList_Null()
        {
            string gatewaySid1 = "aaa";
            string[] devices1 = new string[]{"bbb"};
                
            DevicesMapService devicesMap = new DevicesMapService();
            devicesMap.SetGatewayDevices(gatewaySid1, devices1);
            
            Assert.That(devicesMap.GetGatewaySidForDevice("ccc"), Is.Null);
        }
        
    }
}