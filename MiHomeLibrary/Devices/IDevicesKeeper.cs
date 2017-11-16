using System.Collections.Generic;
using System.Threading.Tasks;
using MiHomeLibrary.Communication;
using MiHomeLibrary.Communication.Responses;

namespace MiHomeLibrary.Devices
{
    public interface IDevicesKeeper
    {
        IEnumerable<BaseDevice> GetDevices();
        Task UpdateDevicesList(DiscoverDeviceRsp response, IMiCommunication communication);
    }
}
