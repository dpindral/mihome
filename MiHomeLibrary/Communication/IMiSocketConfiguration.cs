namespace MiHomeLibrary.Communication
{
    public interface IMiSocketConfiguration
    {
        string MulticastIp { get; }
        int CommunicationPort { get; }
    }
}
