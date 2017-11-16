using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using MiHomeLibrary.Devices;

namespace MiHomeLibrary.Communication
{
    public class KeyGenerator : IKeyGenerator
    {
        private readonly byte[] _iv =
            {0x17, 0x99, 0x6d, 0x09, 0x3d, 0x28, 0xdd, 0xb3, 0xba, 0x69, 0x5a, 0x2e, 0x6f, 0x58, 0x56, 0x2e};

        private readonly IDevicesMap _map;
        private readonly ITokenKeeper _tokenKeeper;
        private readonly IGatewayPasswordKeeper _passwordKeeper;

        public KeyGenerator(IDevicesMap map, ITokenKeeper tokenKeeper, IGatewayPasswordKeeper passwordKeeper)
        {
            _map = map;
            _tokenKeeper = tokenKeeper;
            _passwordKeeper = passwordKeeper;
        }


        public string GenerateKey(string deviceSid)
        {
            string gsid = _map.GetGatewaySidForDevice(deviceSid);
            if (string.IsNullOrWhiteSpace(gsid))
            {
                throw new ArgumentOutOfRangeException(nameof(deviceSid), "device not recognized");
            }
            
            string token = _tokenKeeper.GetTokenForGateway(gsid);
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new CryptographicException("token not found");
            }
            
            string gatewayPassword = _passwordKeeper.GetGatewayPassword(gsid);
            if(string.IsNullOrWhiteSpace(gatewayPassword))
            {
                throw new CryptographicException("password not found");
            }
  

            return GenerateKey(gatewayPassword, token, _iv);
        }
        
        private static string GenerateKey(string password, string token, byte[] iv)
        {
            byte[] key = Encoding.ASCII.GetBytes(password);
            byte[] result;
            
            using (var aes = Aes.Create())
            {
                aes.KeySize = 128;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.None;
                aes.Mode = CipherMode.CBC;
                
                aes.IV = iv;
                aes.Key = key;
                

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cryptoStream = new StreamWriter(new CryptoStream(ms, encryptor, CryptoStreamMode.Write)))
                    {
                        cryptoStream.Write(token);
                    }

                    result = ms.ToArray();
                }
            }
            
            return BitConverter.ToString(result).Replace("-", string.Empty);
        }
        
        
    }
}