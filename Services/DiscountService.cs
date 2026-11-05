using TrainingCenter.Api.Models;
using TrainingCenter.Api.Services.Interfaces;

namespace TrainingCenter.Api.Services
{
    public class DiscountService : IDiscountService
    {
        //Скидка на "Горящие группы"
        private const decimal HotGroupDiscount = 0.20m;
        //Скидка для выпускников
        private const decimal GraduateDiscount = 0.30m;
        //Порог дней для применения скидки для "Горящей группы"
        private const int HotGroupThresholdDays = 5;

        public CartCalculationResult Calculate(IEnumerable<Course> courses, UserProfile userStatus, DateTime? now = null)
        {
            ArgumentNullException.ThrowIfNull(courses);
            ArgumentNullException.ThrowIfNull(userStatus);

            var today = (now ?? DateTime.UtcNow).Date;
            var items = new List<CartItemResult>();

            foreach (var course in courses)
            {
                var daysUntilStart = (course.StartDate.Date - today).TotalDays;

                decimal discount = 0m;

                //Скидка "горящая группа". Если до старта курса осталось менее 5 дней, на него автоматически применяется скидка 20%.
                if (daysUntilStart >= 0 && daysUntilStart < HotGroupThresholdDays)
                {
                    discount = Math.Max(discount, HotGroupDiscount);
                }

                //Скидка выпускника. Если у пользователя в профиле стоит флаг IsGraduate = true, на курсы применяется скидка 30%.
                if (userStatus.IsGraduate)
                {
                    discount = Math.Max(discount, GraduateDiscount);
                }

                // Скидки НЕ суммируются — берём максимум.
                items.Add(new CartItemResult
                {
                    CourseId = course.Id,
                    Title = course.Title,
                    Price = course.Price,
                    FinalPrice = Math.Round(course.Price * (1 - discount), 2),
                    Discount = discount
                });
            }

            return new CartCalculationResult
            {
                Items = items,
                TotalPrice = items.Sum(i => i.Price),
                TotalFinalPrice = items.Sum(i => i.FinalPrice)
            };
        }
    }
}
