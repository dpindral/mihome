using MiHomeLibrary.Communication.Responses;

namespace MiHomeLibrary.Communication
{
    public interface IResponsesParser
    {
        ICommandResponse ParseCommand(string commandString);
    }
}
