using System;
using System.Threading;
using System.Threading.Tasks;
using MiHomeLibrary.Communication.Commands;
using MiHomeLibrary.Communication.Responses;
using MiHomeLibrary.Devices;

namespace MiHomeLibrary.Communication
{
    public class MiCommunication : IMiCommunication
    {
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly IMiTransport _transport;
        private readonly IResponsesParser _parser;
        private readonly IKeyGenerator _keyGenerator;
        private readonly Func<ICommandResponse, Task> _responseHandler;
        

        public MiCommunication(IMiTransport transport, IResponsesParser parser, Func<ICommandResponse, Task> responseHandler, IKeyGenerator keyGenerator)
        {
            _transport = transport;
            _parser = parser;
            _responseHandler = responseHandler;
            _keyGenerator = keyGenerator;
            Task.Run(() => ListenForResponses(_cts.Token), _cts.Token);
        }

        public async Task ExecuteCommand(ICommand command)
        {
            await _transport.SendAsync(command).ConfigureAwait(false);
        }

        public async Task ChangeDeviceState(ChangeCommand command)
        {
            command.data.key = _keyGenerator.GenerateKey(command.sid);
            await ExecuteCommand(command).ConfigureAwait(false);
        }

        private async Task ListenForResponses(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                try
                {
                    string commandString = await _transport.ReceiveAsync().ConfigureAwait(false);
                    //Console.WriteLine(commandString);
                    ICommandResponse respondCommand = _parser.ParseCommand(commandString);

                    if (respondCommand == null)
                    {
                        //Console.WriteLine("listener - command not recognized: "+commandString);
                        continue;
                    }

                    if (_responseHandler == null)
                    {
                        //Console.WriteLine("listener - no one waits for response");
                        continue;
                    }

                    await _responseHandler.Invoke(respondCommand).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Commands listener error: "+ex.Message);
                }
            }
        }

        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
            }
            _transport?.Dispose();
        }
    }
}