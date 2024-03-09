using Grpc.Core;

namespace GrpcSample.Server.Services
{
    public class TikTakService : TikTak.TikTakBase
    {
        private readonly ILogger<TikTakService> _logger;
        public TikTakService(ILogger<TikTakService> logger)
        {
            _logger = logger;
        }

        public override Task<TikTakResponse> Tik(TikTakRequest request, ServerCallContext context)
        {
            _logger.LogInformation("Receiving Tik request with value {Value}", request.Tik);
            string response;
            if(request.Tik.Equals("Tik", StringComparison.InvariantCultureIgnoreCase))
            {
                _logger.LogInformation("Sending Tak");
                response = "Tak";
            } else
            {
                _logger.LogInformation("I do not know what you are talking about");
                response = "What are you talking about?";
            }

            return Task.FromResult(new TikTakResponse
            {
                Tak = response,
            });
        }
    }
}
