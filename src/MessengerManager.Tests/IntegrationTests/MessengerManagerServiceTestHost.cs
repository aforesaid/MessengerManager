using Microsoft.Extensions.Configuration;

namespace MessengerManager.Tests.IntegrationTests
{
    public class MessengerManagerServiceTestHost : MessengerManagerServiceHost
    {
        public MessengerManagerServiceTestHost(IConfiguration configuration) : base(configuration)
        { }
    }
}