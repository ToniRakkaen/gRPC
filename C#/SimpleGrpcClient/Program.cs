using Grpc.Net.Client;
using GrpcService1;

namespace SimpleGrpcClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var grpcChannel = GrpcChannel.ForAddress("https://localhost:7263");
            var greeterClient = new Greeter.GreeterClient(grpcChannel);
            var greeterReply = greeterClient.SayHello(new HelloRequest
            {
                Name = "Khang",
            });
            Console.WriteLine(greeterReply.ToString);
            Console.ReadKey();
        }
    }
}
