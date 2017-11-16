using MiHomeLibrary.Communication.Responses;
using Newtonsoft.Json;

namespace MiHomeLibrary.Communication
{
    public class ResponsesParserService : IResponsesParser
    {
        public ICommandResponse ParseCommand(string commandString)
        {
            if (commandString.Contains("get_id_list_ack"))
            {
                DiscoverGatewayRsp resp = JsonConvert.DeserializeObject<DiscoverGatewayRsp>(commandString);
                return resp;
            }
            else if (commandString.Contains("read_ack") || commandString.Contains("write_ack"))
            {
                DiscoverDeviceRsp resp = JsonConvert.DeserializeObject<DiscoverDeviceRsp>(commandString);
                return resp;
            }
            else if (commandString.Contains("heartbeat"))
            {
                HeartBeatRsp resp = JsonConvert.DeserializeObject<HeartBeatRsp>(commandString);
                return resp;
            }

            return null;
        }
    } 
}