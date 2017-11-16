namespace MiHomeLibrary.Devices
{
    public interface IDevicesMap
    {
        void SetGatewayDevices(string gatewaySid, string[] gatewayDevicesSids);
        string GetGatewaySidForDevice(string deviceSid);
    }
}