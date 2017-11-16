using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MiHomeLibrary.Communication;
using MiHomeLibrary.Communication.Commands;

namespace MiHomeLibrary.Devices
{
    public abstract class BaseDevice
    {
        public string Sid { get; }
        public DeviceTypes Type { get; }
        private string Model { get; }
        public string ShortId { get;}
        public string Name { get; }
        private readonly IMiCommunication _communication; 

        protected BaseDevice(
            string sid, 
            DeviceTypes type, 
            string shortId, 
            string name, 
            string model, 
            IMiCommunication communication)
        {
            Sid = sid;
            Type = type;
            ShortId = shortId;
            Name = string.IsNullOrWhiteSpace(name) ? sid : name;
            Model = model;
            _communication = communication;
        }

        public virtual void UpdateDevice(string data){}

        public virtual void ConsoleInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"  Device ({Sid})");
            Console.WriteLine($"----------------------------");
            Console.WriteLine($"  Name: {Name}");
            Console.WriteLine($"  Type: {Type}");
            Console.WriteLine($"  ShortId: {ShortId}");
        }

        protected async Task ChangeDeviceState(BaseDeviceChangeData changeData)
        {
            ChangeCommand command = new ChangeCommand()
            {
                model = Model,
                sid = Sid,
                data = changeData
            };
            
            await _communication.ChangeDeviceState(command).ConfigureAwait(false);
        }
    }
}
