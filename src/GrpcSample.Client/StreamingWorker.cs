using Grpc.Core;
using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static GrpcSample.Counter;

namespace GrpcSample.Client
{
    internal class StreamingWorker : BackgroundService
    {
        private readonly ILogger<StreamingWorker> _logger;

        public StreamingWorker(ILogger<StreamingWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waiting for server to start");
            await Task.Delay(5000);

            while(!stoppingToken.IsCancellationRequested) {
                _logger.LogInformation("Call streaming service");

                using (var channel = GrpcChannel.ForAddress("https://localhost:7022"))
                {
                    var client = new CounterClient(channel);
                    var request = new CounterRequest()
                    {
                        Start = 0
                    };
                    using (var streamingCall = client.Count(request))
                    {
                        await foreach (var counterResponse in streamingCall.ResponseStream.ReadAllAsync())
                        {
                            _logger.LogInformation("Received counter {Counter}", counterResponse.Count);
                        }
                    };
                }

                await Task.Delay(new Random().Next(5000));
            }
        }
    }
}
