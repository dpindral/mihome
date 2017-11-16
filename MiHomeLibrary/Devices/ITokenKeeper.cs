namespace MiHomeLibrary.Devices
{
    public interface ITokenKeeper
    {
        string GetTokenForGateway(string sid);
        void SetTokenForGateway(string sid, string token);
    }
}