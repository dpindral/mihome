using System;
using System.Threading.Tasks;
using MiHomeLibrary.Communication.Commands;
using MiHomeLibrary.Devices;

namespace MiHomeLibrary.Communication
{
    public interface IMiCommunication : IDisposable
    {
        Task ExecuteCommand(ICommand command);
        Task ChangeDeviceState(ChangeCommand command);
    }
}
