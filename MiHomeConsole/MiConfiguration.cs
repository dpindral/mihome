using System;
using System.Collections.Generic;
using System.Text;
using MiHomeLibrary;
using MiHomeLibrary.Communication;

namespace MiHomeConsole
{
    public class MiConfiguration : IMiSocketConfiguration
    {
        public string MulticastIp => "224.0.0.50";
        public int CommunicationPort => 9898;
    }
}
