using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Services
{
    public class FallbackEmailService : IFallbackEmailService
    {
        private readonly ILogger<FallbackEmailService> _logger;

        public FallbackEmailService(ILogger<FallbackEmailService> logger) => _logger = logger;

        public Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            //Заглушка, здесь мог бы быть реальный вызов SMTP.
            return Task.CompletedTask;
        }
    }
}
