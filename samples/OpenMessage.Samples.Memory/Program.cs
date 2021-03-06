using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenMessage.Samples.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenMessage.Samples.Memory
{
    internal class Program
    {
        private static int _counter;

        private static async Task Main()
        {
            await Host.CreateDefaultBuilder()
                      .ConfigureServices(services => services.AddOptions()
                                                             .AddLogging()
                                                             .AddMassProducerService<SimpleModel>() // Adds a producer that calls configured dispatcher
                      )
                      .ConfigureMessaging(host =>
                      {
                          // Adds a memory based consumer and dispatcher
                          host.ConfigureMemory<SimpleModel>()
                              .Build();

                          // Adds a handler that writes to console every 1000 messages
                          host.ConfigureHandler<SimpleModel>(msg =>
                          {
                              var counter = Interlocked.Increment(ref _counter);

                              if (counter % 1000 == 0)
                                  Console.WriteLine($"Counter: {counter}");
                          });
                      })
                      .Build()
                      .RunAsync();
        }
    }
}