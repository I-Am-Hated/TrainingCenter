using TrainingCenter.Api.Models;

namespace TrainingCenter.Api.Services.Interfaces
{
    public interface IDiscountService
    {
        CartCalculationResult Calculate(IEnumerable<Course> courses, UserProfile user, DateTime? now = null);
    }
}
