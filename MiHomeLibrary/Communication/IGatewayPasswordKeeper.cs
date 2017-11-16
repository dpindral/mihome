namespace MiHomeLibrary.Communication
{
    public interface IGatewayPasswordKeeper
    {
        string GetGatewayPassword(string sid);
    }
}