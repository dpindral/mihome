using System;
using System.Collections.Generic;
using System.Linq;

namespace MiHomeLibrary.Devices
{
    public class TokenKeeperService : ITokenKeeper
    {
        private readonly List<GatewayToken> _tokens = new List<GatewayToken>();
        private static readonly object Locker = new object();

        public string GetTokenForGateway(string sid)
        {
            
            lock (Locker)
            {
                var gt = _tokens.FirstOrDefault(x => x.Sid == sid);
                return gt?.Token;
            }
        }

        public void SetTokenForGateway(string sid, string token)
        {
            if (string.IsNullOrWhiteSpace(sid))
            {
                throw new ArgumentException(nameof(sid));
            }
            
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException(nameof(sid));
            }
            
            lock (Locker)
            {
                GatewayToken gt = _tokens.FirstOrDefault(x => x.Sid == sid);

                if (gt == null)
                {
                    gt = new GatewayToken()
                    {
                        Sid = sid,
                        Token = token
                    };
                    _tokens.Add(gt);
                }

                gt.Token = token;                
            }
            
        }
    }
}