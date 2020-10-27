using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cocona;
using Fib.Api;
using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.DependencyInjection;

namespace Fib.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch( "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            await CoconaApp.Create()
                .ConfigureServices(services =>
                {
                    services.AddGrpcClient<FibCalculator.FibCalculatorClient>(options =>
                    {
                        options.Address = new Uri("http://localhost:5000");
                        options.ChannelOptionsActions.Add(channelOptions => channelOptions.Credentials = ChannelCredentials.Insecure);
                    });
                })
                .RunAsync<Program>(args);
        }

        public async Task Run([FromService]FibCalculator.FibCalculatorClient client)
        {
            var reply = await client.CalculateAsync(new CalculateRequest() {Num = 50});
            Console.WriteLine("Greeting: " + reply.Result);
        }
    }
}