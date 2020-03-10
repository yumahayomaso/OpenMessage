using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenMessage.AWS.SNS;
using OpenMessage.AWS.SQS;
using OpenMessage.Samples.Core.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using OpenMessage.AWS.SQS.Configuration;

namespace OpenMessage.Samples.AWS
{
    internal class Program
    {
        private static int _counter;

        private static async Task Main(string[] args)
        {
            // Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", "XXX", EnvironmentVariableTarget.Process);
            // Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", "XXX", EnvironmentVariableTarget.Process);
            // Environment.SetEnvironmentVariable("AWS_SESSION_TOKEN", "XXX", EnvironmentVariableTarget.Process);
            // Environment.SetEnvironmentVariable("AWS_DEFAULT_REGION", "us-east-1", EnvironmentVariableTarget.Process);

            await Host.CreateDefaultBuilder()
                      .ConfigureServices(services => services.AddOptions()
                                                             .AddLogging()
                                                             .AddSampleCore()
                                                             //.AddProducerService<SimpleModel>() // Adds a producer that calls configured dispatcher
                      )
                      .ConfigureMessaging(host =>
                      {
                          host.Services.Configure<SQSDispatcherOptions>(x => x.RegionEndpoint = "eu-west-1");

                          // Adds a handler that writes to console every 1000 messages
                          host.ConfigureHandler<CoreModel>(msg =>
                          {
                              var counter = Interlocked.Increment(ref _counter);

                              if (counter % 100 == 0)
                                  Console.WriteLine($"Counter: {counter}");
                          });

                          // Allow us to write to kafka
                          host.ConfigureSqsDispatcher<SimpleModel>()
                              .FromConfiguration(config =>
                              {
                                  config.QueueUrl = "https://sqs.eu-west-1.amazonaws.com/528130383285/stu_test";
                                  // config.TopicArn = "arn:aws:sns:us-east-1:000000000000:openmessage_samples_core_models_simplemodel";
                                  // config.ServiceURL = "http://localhost:4575";
                              })
                              .Build();

                          // Consume from the same topic as we are writing to
                          host.ConfigureSqsConsumer<CoreModel>()
                              .FromConfiguration(config =>
                              {
                                  config.QueueUrl = "https://sqs.eu-west-1.amazonaws.com/528130383285/stu_test";
                                  config.RegionEndpoint = "eu-west-1";
                                  //config.QueueUrl = "http://localhost:4576/queue/openmessage_samples_core_models_simplemodel.queue";
                                  //config.ServiceURL = "http://localhost:4576";
                              })
                              .Build();
                      })
                      .Build()
                      .RunAsync();
        }
    }
}