namespace TrainingCenter.Api.Services.Interfaces
{
    public interface IFallbackEmailService
    {
        Task SendAsync(string to, string subject, string body, CancellationToken ct = default);
    }
}
