using System;
using System.Collections.Generic;
using System.Text;
using MiHomeLibrary.Communication;
using MiHomeLibrary.Communication.Responses;
using Newtonsoft.Json;

namespace MiHomeLibrary.Devices
{
    public class TemperatureSensor : BaseDevice
    {
        public float Temperature { get; set; }
        public float Humidity { get; set; }
        public float Voltage { get; set; }

        public TemperatureSensor(DiscoverDeviceRsp response, string name, IMiCommunication communication) 
            : base(response.sid, DeviceTypes.TemperatureHumiditySensor, response.short_id, name, response.model, communication)
        {
        }

        public override void UpdateDevice(string data)
        {
            base.UpdateDevice(data);

            TemperatureSensorProps props = JsonConvert.DeserializeObject<TemperatureSensorProps>(data);
            Temperature = ConvertToFloat(props.temperature);
            Humidity = ConvertToFloat(props.humidity);
            Voltage = ConvertToFloat(props.voltage);
        }

        private float ConvertToFloat(int value)
        {
            return 1.0f * value / 100;
        }

        public override void ConsoleInfo()
        {
            base.ConsoleInfo();
            Console.WriteLine($"  Temperature: {Temperature}°C");
            Console.WriteLine($"  Humidity: {Humidity}%");
            Console.WriteLine($"  Battery: {Voltage}%");
        }
    }

    
}
