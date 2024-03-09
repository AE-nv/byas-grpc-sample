using Grpc.Net.Client;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using static GrpcSample.TikTak;

namespace GrpcSample.Client
{
    internal class TikTakWorker : BackgroundService
    {
        private readonly ILogger<TikTakWorker> _logger;

        public TikTakWorker(ILogger<TikTakWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Waiting for server to start");
            await Task.Delay(5000);
            string nextRequest = "Tik";

            while (!stoppingToken.IsCancellationRequested) {
                _logger.LogInformation("Call Tik service with value {Value}", nextRequest);

                using (var channel = GrpcChannel.ForAddress("https://localhost:7022"))
                {
                    var client = new TikTakClient(channel);
                    var request = new TikTakRequest()
                    {
                        Tik = nextRequest
                    };

                    var response = await client.TikAsync(request);

                    _logger.LogInformation("Response: {Response}", response.Tak);
                }

                nextRequest = nextRequest.Equals("Tik", StringComparison.InvariantCultureIgnoreCase) ? "Unknown" : "Tik";
                await Task.Delay(new Random().Next(5000));
            }
        }
    }
}
