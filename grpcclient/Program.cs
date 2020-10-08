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


        static async Task ccc(HttpClient client, string token)
        {
            client.SetBearerToken(token);
            var response = await client.GetAsync("https://192.168.31.114/api/Home");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
        }


        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HttpClient client2 = new HttpClient();

            string url = "http://192.168.31.114:5000/connect/token";

            var tokenResponse = await client2.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                //Address = disco.TokenEndpoint,
                Address = url,
                ClientId = "ro.client",
                ClientSecret = "secret",
                Scope = "api1",
                UserName = "icxl",
                Password = "123456",
            });

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            string token = tokenResponse.AccessToken;
            await ccc(client2, token);

            Metadata data = new Metadata();
            data.Add("Authorization", $"Bearer {token}");

            using var channel = GrpcChannel.ForAddress("https://192.168.31.114:443");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                              new HelloRequest { Name = "123213123" }, data);



            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
