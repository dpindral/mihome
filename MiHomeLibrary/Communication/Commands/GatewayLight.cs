namespace MiHomeLibrary.Communication.Commands
{
    public class GatewayLight : BaseDeviceChangeData
    {
        public long rgb { get; set; }
        public int illumination { get; set; }
    }
}