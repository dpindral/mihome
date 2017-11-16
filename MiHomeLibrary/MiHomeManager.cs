using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MiHomeLibrary.Communication;
using MiHomeLibrary.Communication.Commands;
using MiHomeLibrary.Communication.Responses;
using MiHomeLibrary.Devices;
using Newtonsoft.Json;

namespace MiHomeLibrary
{
    public class MiHomeManager : IDisposable
    {
        private readonly IMiCommunication _communication;
        private readonly IDevicesKeeper _devicesKeeper;
        private readonly ITokenKeeper _tokenKeeper; 
        private readonly IDevicesMap _devicesMap;

        private AutoResetEvent _waitHandle;
        private int _awaitedDevices;

        public IEnumerable<BaseDevice> Devices => _devicesKeeper.GetDevices();

        public MiHomeManager(
            IMiCommunicationFactory communicationFactory, 
            IDevicesKeeper devicesKeeper, 
            ITokenKeeper tokenKeeper, 
            IDevicesMap devicesMap,
            IKeyGeneratorFactory keyGeneratorFactory)
        {
            _devicesKeeper = devicesKeeper;
            _tokenKeeper = tokenKeeper;
            _devicesMap = devicesMap;
            var keyGenerator = keyGeneratorFactory.Build(_devicesMap, _tokenKeeper);
            _communication = communicationFactory.Build(CommandListener,keyGenerator);
        }

        public async Task DiscoverHome()
        {
            _waitHandle = new AutoResetEvent(false);
            _awaitedDevices = 0;
 
            await _communication.ExecuteCommand(new DiscoverGatewayCmd());

            _waitHandle.WaitOne(10000);
        }

        private async Task CommandListener(ICommandResponse command)
        {
            if (command is DiscoverGatewayRsp)
            {
                await GatewayDiscovered(command as DiscoverGatewayRsp);
            }
            else if (command is DiscoverDeviceRsp)
            {
                await UpdateDevice(command as DiscoverDeviceRsp);
            }
            else if (command is HeartBeatRsp)
            {
                await HeartBeat(command as HeartBeatRsp);
            }
        }

        private async Task HeartBeat(HeartBeatRsp heartBeatRsp)
        {
            _tokenKeeper.SetTokenForGateway(heartBeatRsp.sid,heartBeatRsp.token);
        }

        private async Task GatewayDiscovered(DiscoverGatewayRsp response)
        {
            var sids = JsonConvert.DeserializeObject<string[]>(response.data);
            
            Interlocked.Add(ref _awaitedDevices, 1 + sids.Length);
          
            
            _tokenKeeper.SetTokenForGateway(response.sid, response.token);
            _devicesMap.SetGatewayDevices(response.sid, sids);

            await _communication.ExecuteCommand(new DiscoverDeviceCmd(response.sid));
            foreach (string sid in sids)
            {
                await _communication.ExecuteCommand(new DiscoverDeviceCmd(sid));
            }
        }

        private async Task UpdateDevice(DiscoverDeviceRsp response)
        {
            await _devicesKeeper.UpdateDevicesList(response, _communication);

            Interlocked.Decrement(ref _awaitedDevices);
            if (_awaitedDevices == 0)
            {
                _waitHandle.Set();
            }
        }

        public void Dispose()
        {
            _communication?.Dispose();
        }
    }
}
