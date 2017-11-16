using System;
using System.Threading.Tasks;
using MiHomeLibrary.Communication.Responses;

namespace MiHomeLibrary.Communication
{
    public interface IMiCommunicationFactory
    {
        IMiCommunication Build(Func<ICommandResponse, Task> responseHandler, IKeyGenerator keyGenerator);
    }
}