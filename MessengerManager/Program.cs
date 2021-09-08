using System;
using Microsoft.Extensions.Configuration;

namespace MessengerManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>()
                .Build();
            
            var host = new MessengerManagerServiceHost(config);
            
        }
    }
}