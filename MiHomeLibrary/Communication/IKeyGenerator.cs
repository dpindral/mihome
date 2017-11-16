namespace MiHomeLibrary.Communication
{
    public interface IKeyGenerator
    {
        string GenerateKey(string deviceSid);
    }
}