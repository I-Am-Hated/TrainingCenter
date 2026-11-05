namespace TrainingCenter.Api.Models
{
    public class CartItemResult
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public string CourseId { get; init; } = string.Empty;
        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; init; } = string.Empty;
        /// <summary>
        /// Цена курса
        /// </summary>
        public decimal Price { get; init; }
        /// <summary>
        /// Цена курса с учётом скидки
        /// </summary>
        public decimal FinalPrice { get; init; }
        /// <summary>
        /// Скидка
        /// </summary>
        public decimal Discount { get; init; }
    }
}
