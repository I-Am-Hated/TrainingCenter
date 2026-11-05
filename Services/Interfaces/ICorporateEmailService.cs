namespace TrainingCenter.Api.Services.Interfaces
{
    public interface ICorporateEmailService
    {
        Task SendAsync(string to, string subject, string body, CancellationToken ct = default);
    }
}
