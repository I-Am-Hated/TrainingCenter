using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Services
{
    public class CorporateEmailService : ICorporateEmailService
    {
        private readonly ILogger<CorporateEmailService> _logger;

        public CorporateEmailService(ILogger<CorporateEmailService> logger) => _logger = logger;

        public Task SendAsync(string to, string subject, string body, CancellationToken ct = default)
        {
            //Имитация недоступности корпоративного почтового сервера
            _logger.LogWarning("Ошибка отправки почты. Коропоративный почтовый сервер недоступен");
            throw new InvalidOperationException("Коропоративный почтовый сервер недоступен");
        }
    }
}
