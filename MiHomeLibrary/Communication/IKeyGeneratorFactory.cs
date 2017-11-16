using MiHomeLibrary.Devices;

namespace MiHomeLibrary.Communication
{
    public interface IKeyGeneratorFactory
    {
        IKeyGenerator Build(IDevicesMap map, ITokenKeeper tokenKeeper);
    }
    
    public class KeyGeneratorFactory : IKeyGeneratorFactory{
        private readonly IGatewayPasswordKeeper _passwordKeeper;

        public KeyGeneratorFactory(IGatewayPasswordKeeper passwordKeeper)
        {
            _passwordKeeper = passwordKeeper;
        }

        public IKeyGenerator Build(IDevicesMap map, ITokenKeeper tokenKeeper)
        {
            return new KeyGenerator(map,tokenKeeper,_passwordKeeper);
        }
    }
}