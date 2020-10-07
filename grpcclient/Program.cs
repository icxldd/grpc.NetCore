using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcGreeter;
using IdentityModel.Client;
using Microsoft.Extensions.Primitives;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace grpcclient
{
    class Program
    {
      

        static async System.Threading.Tasks.Task Main(string[] args)
        {
         
            using var channel = GrpcChannel.ForAddress("https://192.168.31.114:443");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "123213123" });



            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
