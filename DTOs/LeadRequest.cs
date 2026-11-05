using System.ComponentModel.DataAnnotations;
using TrainingCenter.Api.DTOs.Enums;

namespace TrainingCenter.Api.DTOs
{
    public class LeadRequest
    {
        [Required(ErrorMessage = "Имя обязательно")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Телефон обязателен")]
        //Вариант с мягкой проверкой тедефона, если на фронте нет вообще никакой валидации
        //[RegularExpression(@"^\+?[0-9\s\-\(\)]{10,20}$", ErrorMessage = "Некорректный формат телефона")]
        [RegularExpression(@"^\+7\d{10}$", ErrorMessage = "Некорректный формат телефона")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Тип юридического лица обязателен")]
        [EnumDataType(typeof(ClientType))]
        public ClientType ClientType { get; set; }
    }
}
