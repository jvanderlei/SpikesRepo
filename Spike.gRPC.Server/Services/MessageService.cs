using Grpc.Core;

namespace Spike.gRPC.Server.Services
{
    public class MessageService : Comm.CommBase
    {
        private ILogger<MessageService> _logger;

        public MessageService(ILogger<MessageService> logger)
        {
            _logger = logger;
        }

        public override async Task BiWayStreaming(IAsyncStreamReader<ClientMessage> requestStream,
            IServerStreamWriter<ServerMessage> responseStream, ServerCallContext context)
        {
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    Console.WriteLine(message.ToString());
                }
            });

            while (!readTask.IsCompleted)
            {
             
                await responseStream.WriteAsync(new ServerMessage() { Message = "Hello World! pra não dar azar, mas salve"});
                await Task.Delay(10000);

            }
        }
    }
}
