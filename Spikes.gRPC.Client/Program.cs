// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using Spike.gRPC.Server;

Console.WriteLine("Hello, World!");

using var channel = GrpcChannel.ForAddress("https://localhost:7019/");
var client = new Comm.CommClient(channel);
using var call = client.BiWayStreaming();

Console.WriteLine("Iniciando leitura de mensagens");

var readTask = Task.Run(async () =>
{
    await foreach(var response in call.ResponseStream.ReadAllAsync())
    {
        Console.WriteLine(response.Message);
    }
});

Console.WriteLine("Começando Stream de mensagens");
while (true)
{
    var message = Console.ReadLine();
    if (string.IsNullOrEmpty(message))
    {
        break;
    }

    await call.RequestStream.WriteAsync(new ClientMessage { Message = message });
}

Console.WriteLine("Concluído");
await call.RequestStream.CompleteAsync();
await readTask;
