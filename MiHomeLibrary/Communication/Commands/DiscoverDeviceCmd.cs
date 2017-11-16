namespace MiHomeLibrary.Communication.Commands
{
    class DiscoverDeviceCmd : ICommand
    {
        private readonly string _sid;

        public DiscoverDeviceCmd(string sid)
        {
            _sid = sid;
        }

        public string SerializeCommand()
        {
            return $"{{\"cmd\":\"read\",\"sid\":\"{_sid}\"}}";
        }
    }
}
