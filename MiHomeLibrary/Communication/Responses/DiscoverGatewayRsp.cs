namespace MiHomeLibrary.Communication.Responses
{
    class DiscoverGatewayRsp : ICommandResponse
    {
        public string cmd { get; set; }
        public string sid { get; set; }
        public string token { get; set; }
        public string data { get; set; }
    }
}
