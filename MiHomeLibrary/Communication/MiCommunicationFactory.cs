using System;
using System.Threading.Tasks;
using MiHomeLibrary.Communication.Responses;

namespace MiHomeLibrary.Communication
{
    public class MiCommunicationFactory : IMiCommunicationFactory
    {
        private readonly IMiTransport _transport;
        private readonly IResponsesParser _parser;

        public MiCommunicationFactory(IMiTransport transport, IResponsesParser parser)
        {
            _transport = transport;
            _parser = parser;
        }

        public IMiCommunication Build(Func<ICommandResponse, Task> responseHandler, IKeyGenerator keyGenerator)
        {
            return new MiCommunication(_transport,_parser, responseHandler, keyGenerator);
        }
    }
}