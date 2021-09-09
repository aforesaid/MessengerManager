using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MessengerManager
{
    class Program
    {
        static async Task Main()
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("config.json")
                .AddUserSecrets<Program>()
                .Build();
            
            using var cancellationSource = new CancellationTokenSource();

            var host = new MessengerManagerServiceHost(config);
            await host.Start(cancellationSource.Token);
            Console.ReadKey();
        }
    }
}