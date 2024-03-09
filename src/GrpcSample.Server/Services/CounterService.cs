using Grpc.Core;

namespace GrpcSample.Server.Services
{
    public class CounterService : Counter.CounterBase
    {
        private readonly ILogger<CounterService> _logger;
        public CounterService(ILogger<CounterService> logger)
        {
            _logger = logger;
        }

        public override async Task Count(CounterRequest request, IServerStreamWriter<CounterResponse> responseStream, ServerCallContext context)
        {
            _logger.LogInformation("Received request with start {Start}", request.Start);
            var counter = request.Start;
            for (int i = 0; i < 10; i++)
            {
                _logger.LogInformation("Sending value {Value}", counter);
                await responseStream.WriteAsync(new CounterResponse()
                {
                    Count = counter
                });
                counter++;
                await Task.Delay(1000);
            }
        }
    }
}
