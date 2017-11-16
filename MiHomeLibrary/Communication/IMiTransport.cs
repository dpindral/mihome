using System;
using System.Threading.Tasks;
using MiHomeLibrary.Communication.Commands;

namespace MiHomeLibrary.Communication
{
    public interface IMiTransport : IDisposable
    {
        Task SendAsync(ICommand command);
        Task<string> ReceiveAsync();
    }
}
