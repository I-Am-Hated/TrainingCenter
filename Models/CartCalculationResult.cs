namespace TrainingCenter.Api.Models
{
    public class CartCalculationResult
    {
        public IReadOnlyList<CartItemResult> Items { get; init; } = Array.Empty<CartItemResult>();
        public decimal TotalPrice { get; init; }
        public decimal TotalFinalPrice { get; init; }
    }
}
