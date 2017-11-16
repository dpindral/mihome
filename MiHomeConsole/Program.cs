using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using MiHomeLibrary;
using MiHomeLibrary.Devices;

namespace MiHomeConsole
{
    class Program
    {
        private static IContainer Container { get; set; }

        public static void Main(string[] args)
        {
            DiConfiguration();

            MainAsync(args).GetAwaiter().GetResult();

            
        }

        public static async Task MainAsync(string[] args)
        {
            MiHomeManager home = Container.Resolve<MiHomeManager>();

            await home.DiscoverHome();
            Console.WriteLine("-- home discovered --");

            

            foreach (var device in home.Devices)
            {
                device.ConsoleInfo();
                
            }

            Console.ReadKey();

            if (home.Devices.FirstOrDefault(x=>x is Gateway) is Gateway g)
            {
                Console.WriteLine("-- light red --");
                await g.TurnLightOn(255,0,0,1299).ConfigureAwait(false);
                Thread.Sleep(200);
                g.ConsoleInfo();
                Console.ReadKey();
                
                Console.WriteLine("-- light green --");
                await g.TurnLightOn(0,255,0,1299).ConfigureAwait(false);
                Thread.Sleep(200);
                g.ConsoleInfo();
                Console.ReadKey();
                
                Console.WriteLine("-- light blue --");
                await g.TurnLightOn(0,0,255,1299).ConfigureAwait(false);
                Thread.Sleep(200);
                g.ConsoleInfo();
                Console.ReadKey();
                
                Console.WriteLine("-- light off --");
                await g.TurnLightOff().ConfigureAwait(false);
                Thread.Sleep(200);
                g.ConsoleInfo();
                Console.ReadKey();
            }

            

            home.Dispose();
            
        }

        private static void DiConfiguration()
        {
            ContainerBuilder builder = new ContainerBuilder();
            var dataAccess = Assembly.Load("MiHomeLibrary");
            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(dataAccess)
                .Where(t => t.Name.EndsWith("Factory"))
                .AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterType<MiHomeManager>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterType<MiConfiguration>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            
            builder.RegisterType<GatewayPasswordKeeper>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            Container = builder.Build();
        }
    }
}
