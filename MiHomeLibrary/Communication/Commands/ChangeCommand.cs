using Newtonsoft.Json;

namespace MiHomeLibrary.Communication.Commands
{
    public class ChangeCommand : ICommand
    {
        public string cmd => "write";
        public string sid { get; set; }
        public string model { get; set; }
        public BaseDeviceChangeData data { get; set; }
        
        public string SerializeCommand()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}