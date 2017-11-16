namespace MiHomeLibrary.Communication.Commands
{
    internal class DiscoverGatewayCmd : ICommand
    {
        public string SerializeCommand()
        {
            return "{\"cmd\":\"get_id_list\"}";
        }
    }
}
