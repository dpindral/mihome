namespace MiHomeLibrary.Communication.Responses
{
    sealed class TemperatureSensorProps
    {
        public int voltage { get; set; }
        public int temperature { get; set; }
        public int humidity { get; set; }
    }
}