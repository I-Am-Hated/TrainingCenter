using TrainingCenter.Api.DTOs;
using TrainingCenter.Api.DTOs.Enums;
using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Services
{
    public class LeadService : ILeadService
    {
        //Основной(корпоративный) email для отправки уведомлений
        private const string MainLeadEmail = "test.email@specialist.ru";
        //Резервный email для юрлиц
        private const string LegalEntityFallbackEmail = "specialist.corp@yandex.ru";
        //Резервный email для физлиц
        private const string IndividualFallbackEmail = "specialist.ufl@yandex.ru";

        private readonly ICorporateEmailService _corporateEmailService;
        private readonly IFallbackEmailService _fallbackService;
        private readonly ILogger<LeadService> _logger;

        public LeadService(
            ICorporateEmailService corporateEmailService,
            IFallbackEmailService fallbackService,
            ILogger<LeadService> logger)
        {
            _corporateEmailService = corporateEmailService;
            _fallbackService = fallbackService;
            _logger = logger;
        }

        public async Task SubmitAsync(LeadRequest request, CancellationToken ct = default)
        {
            var subject = $"Новая заявка — {request.ClientType}";
            var body = BuildEmailBody(request);

            try
            {
                await _corporateEmailService.SendAsync(MainLeadEmail, subject, body, ct);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Не удалось отправить email на адрес {Email} через копоративную почту, переключение на резервный канал доставки", request.Email);

                var fallbackRecipient = request.ClientType == ClientType.LegalEntity
                    ? LegalEntityFallbackEmail
                    : IndividualFallbackEmail;

                try
                {
                    await _fallbackService.SendAsync(fallbackRecipient, subject, body, ct);
                }
                catch (Exception fallbackEx)
                {
                    _logger.LogError(fallbackEx,
                        "Не удалось отправаить email на почту {Email} через резервный канал", request.Email);
                }
            }
        }

        private static string BuildEmailBody(LeadRequest r) =>
            $"""
            Имя: {r.Name}
            Телефон: {r.Phone}
            Email: {r.Email}
            Тип клиента: {r.ClientType}
            """;
    }
}
