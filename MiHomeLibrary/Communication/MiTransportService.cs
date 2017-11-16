using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MiHomeLibrary.Communication.Commands;

namespace MiHomeLibrary.Communication
{
    public class MiTransportService : IMiTransport 
    {
        private readonly IMiSocketConfiguration _configuration;
        private readonly Socket _socket;

        public MiTransportService(IMiSocketConfiguration configuration)
        {
            _configuration = configuration;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse(configuration.MulticastIp)));
            _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Any, configuration.CommunicationPort));
        }

        public async Task SendAsync(ICommand data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            var buffer = Encoding.ASCII.GetBytes(data.SerializeCommand());

            _socket.SendTo(buffer, 0, buffer.Length, 0, new IPEndPoint(IPAddress.Parse(_configuration.MulticastIp), _configuration.CommunicationPort));
        }

        public async Task<string> ReceiveAsync()
        {
            var data = new ArraySegment<byte>(new byte[1024]);

            var len = await _socket.ReceiveAsync(data, SocketFlags.None).ConfigureAwait(false);

            return Encoding.ASCII.GetString(data.Array, 0, len);
        }

        public void Dispose()
        {
            _socket?.Dispose();
        }
    }
}