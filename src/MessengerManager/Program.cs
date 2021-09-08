using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace MessengerManager
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("config.json")
                .AddUserSecrets<Program>()
                .Build();
            var cancellationSource = new CancellationTokenSource();
            
            try
            {
                var host = new MessengerManagerServiceHost(config);
                await host.Start(cancellationSource.Token);
                Console.ReadKey();
            }
            finally
            {
                cancellationSource.Dispose();
            }
        }
    }
}