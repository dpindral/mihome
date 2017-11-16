using System.Collections.Generic;
using MiHomeLibrary.Communication;

namespace MiHomeConsole
{
    public class GatewayPasswordKeeper : IGatewayPasswordKeeper
    {
        private Dictionary<string, string> passwords =
            new Dictionary<string, string>() {{"34ce0090806a", "jqdtpdq5s3zdizpd"}};
        
        public string GetGatewayPassword(string sid)
        {
            if (passwords.ContainsKey(sid))
            {
                return passwords[sid];
            }

            return null;
        }
    }
}