namespace MiHomeLibrary.Communication.Responses
{
    public class HeartBeatRsp : ICommandResponse
    {
        public string cmd { get; set; }
        public string model { get; set; }
        public string sid { get; set; }
        public string short_id { get; set; }
        public string token { get; set; }
        public string data { get; set; }
    }
}