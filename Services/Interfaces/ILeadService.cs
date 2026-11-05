using TrainingCenter.Api.DTOs;

namespace TrainingCenter.Api.Services.Interfaces
{
    public interface ILeadService
    {
        Task SubmitAsync(LeadRequest request, CancellationToken ct = default);
    }
}
