namespace TrainingCenter.Api.Models
{
    public class Course
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public string Id { get; set; } = string.Empty;
        /// <summary>
        /// Наименование
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// Цена
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartDate { get; set; }
    }
}
