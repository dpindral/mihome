using System;
using System.Threading.Tasks;
using MiHomeLibrary.Communication;
using MiHomeLibrary.Communication.Commands;
using MiHomeLibrary.Communication.Responses;
using Newtonsoft.Json;

namespace MiHomeLibrary.Devices
{
    public class Gateway : BaseDevice
    {
        public long Rgb { get; private set; }
        public int Illumination { get; private set; }
        public string Version { get; private set; }

        public Gateway(DiscoverDeviceRsp response, string name,IMiCommunication communication)
            : base(response.sid, DeviceTypes.Gateway, response.short_id, name, response.model,communication)
        {
        }

        public override void UpdateDevice(string data)
        {
            base.UpdateDevice(data);

            GatewayProps props = JsonConvert.DeserializeObject<GatewayProps>(data);
            Rgb = props.rgb;
            Illumination = props.illumination;
            Version = props.proto_version;
        }

        public override void ConsoleInfo()
        {
            base.ConsoleInfo(); 
            Console.WriteLine($"  Rgb: {Rgb}");
            Console.WriteLine($"  Illumination: {Illumination}");
            Console.WriteLine($"  Version: {Version}");
        }

        public async Task TurnLightOn(byte r, byte g, byte b, int illumination)
        {
            var rgb = 0xFF000000 | r << 16 | g << 8 | b; 
            if (illumination < 300 || illumination > 1300) throw new ArgumentException("Illumination must be in range 300 - 1300");
            
            GatewayLight newState = new GatewayLight()
            {
                rgb = rgb,
                illumination = illumination
            };
            await base.ChangeDeviceState(newState).ConfigureAwait(false);
        }

        public async Task TurnLightOff()
        {
            GatewayLight newState = new GatewayLight()
            {
                rgb = 0,
                illumination = Illumination
            };
            await base.ChangeDeviceState(newState).ConfigureAwait(false);
        }
    }
}
