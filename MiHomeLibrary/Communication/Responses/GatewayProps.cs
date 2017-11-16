namespace MiHomeLibrary.Communication.Responses
{
    sealed class GatewayProps
    {
        public long rgb { get; set; }
        public int illumination { get; set; }
        public string proto_version { get; set; }
    }
}