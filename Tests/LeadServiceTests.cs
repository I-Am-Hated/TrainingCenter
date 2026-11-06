using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using TrainingCenter.Api.DTOs;
using TrainingCenter.Api.DTOs.Enums;
using TrainingCenter.Api.Services;
using TrainingCenter.Api.Services.Interfaces;
using Xunit;

namespace TrainingCenter.Tests
{
    public class LeadServiceTests
    {
        private static LeadService BuildService(Mock<ICorporateEmailService> corporateEmailService, Mock<IFallbackEmailService> fallbackEmailService)
            => new LeadService(corporateEmailService.Object, fallbackEmailService.Object, NullLogger<LeadService>.Instance);

        private static LeadRequest CreateLoadRequest(ClientType type) => new()
        {
            Name = "Тестовый пользователь",
            Phone = "+79001234567",
            Email = "user@test.ru",
            ClientType = type
        };

        [Fact]
        public async Task SubmitAsync_CorporateFails_Individual_UsesUflFallback()
        {
            var corporateEmailService = new Mock<ICorporateEmailService>();

            corporateEmailService.Setup(p => p.SendAsync(It.IsAny<string>(), It.IsAny<string>(),
                                           It.IsAny<string>(), It.IsAny<CancellationToken>()))
                   .ThrowsAsync(new InvalidOperationException("Корпоративный сервер недоступен"));

            var fallbackEmailService = new Mock<IFallbackEmailService>();

            var service = BuildService(corporateEmailService, fallbackEmailService);

            await service.SubmitAsync(CreateLoadRequest(ClientType.Individual));

            fallbackEmailService.Verify(f => f.SendAsync(
                "specialist.ufl@yandex.ru",
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task SubmitAsync_CorporateFails_LegalEntity_UsesCorpFallback()
        {
            var corporateEmailService = new Mock<ICorporateEmailService>();

            corporateEmailService.Setup(p => p.SendAsync(It.IsAny<string>(), It.IsAny<string>(),
                                           It.IsAny<string>(), It.IsAny<CancellationToken>()))
                   .ThrowsAsync(new InvalidOperationException("Корпоративный сервер недоступен"));

            var fallbackEmailService = new Mock<IFallbackEmailService>();

            var leadService = BuildService(corporateEmailService, fallbackEmailService);
            await leadService.SubmitAsync(CreateLoadRequest(ClientType.LegalEntity));

            fallbackEmailService.Verify(f => f.SendAsync(
                "specialist.corp@yandex.ru",
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
